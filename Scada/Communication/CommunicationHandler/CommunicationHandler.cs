using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Common.ICommunication;
using Common.Message;
using Common.TaskHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        private TaskHandler reciverTask;
        private TaskHandler sendingTask;
        private TaskHandler connectionTask;

        private byte[] currentlySendingBytes;
        private CancellationTokenSource cts = new CancellationTokenSource();
        public BlockingCollection<byte[]> dataToSend=new BlockingCollection<byte[]>();

        private IMessageHandler messageHandler;
        private ICommunicationStream communicationStream;
        private ICommunicationHandlerOptions options;
        private IAsyncSecureCommunication secureCommunication;
        private IStateHandler<CommunicationState> stateHandler= new StateHandler<CommunicationState>();

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions, ICommunicationOptions communicationOptions)
        {
            options = communicationHandlerOptions;

            stateHandler.StateChanged += connectionStateChanged;

            if (communicationOptions.CommunicationType == CommunicationType.TCP) { communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions); }
            
            if (options.SecurityMode == SecurityMode.SECURE) { secureCommunication = new SecureCommunication(); }

            if (options.MessageType == MessageType.TCPModbus){ messageHandler = new TCPModbusMessageHandler(false,dataToSend);}

            if (options.ReconnectInterval > 0)
                connectionTask = new TaskHandler(connectTheStream, false,options.ReconnectInterval,cts);
            else
                connectionTask = new TaskHandler(connectTheStream, true, options.ReconnectInterval,cts);

            reciverTask = new TaskHandler(recivingData, false, 0, cts);
            sendingTask = new TaskHandler(sendingData, false, 0, cts);

            connectionTask.TaskShouldContinue();
        }

        public async Task connectTheStream()
        {
            try
            {
                await communicationStream.Connect();

                if (options.SecurityMode == SecurityMode.SECURE)
                {
                    communicationStream.Stream = await secureCommunication.SecureStream(communicationStream.Stream);
                }

                stateHandler.ChangeState(CommunicationState.CONNECTED);
            }
            catch (Exception ex) when (ex is UnsuccessfullConnectionException || ex is ConnectionAlreadyExisting || ex is AuthenticationFailedException)
            {
                Console.WriteLine(ex.Message);  
                stateHandler.ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);
            }
        }

        private async Task sendingData()
        {
            try
            {
                if (currentlySendingBytes == null)            //like that we can't lose data bcs of connection error
                    currentlySendingBytes = dataToSend.Take();

                if (stateHandler.State != CommunicationState.CONNECTED) //need 1 more check bcs if task got started, we waiting for message,and then disconnect happens
                    return;

                await communicationStream.Send(currentlySendingBytes);

                currentlySendingBytes = null;
            }
            catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
            {
                Console.WriteLine(ex.Message);
                stateHandler.ChangeState(CommunicationState.DISCONNECTED);
            }
            catch (Exception ex) when (ex is ObjectDisposedException) { };
        }

        private async Task recivingData()
        {
            byte[] recivedData;

            try
            {
                recivedData = await communicationStream.Receive();

                messageHandler.CreateMessageObject(recivedData);
            }
            catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
            {
                Console.WriteLine(ex.Message);
                stateHandler.ChangeState(CommunicationState.DISCONNECTED);
            }
            catch (Exception ex) when (ex is ObjectDisposedException){ }; 
        }

        private void connectionStateChanged()
        {
            Console.WriteLine("State changed to " + stateHandler.State);
            if (stateHandler.State == CommunicationState.CONNECTED)
            {
                connectionTask.TaskShouldWait();
                sendingTask.TaskShouldContinue();
                reciverTask.TaskShouldContinue();
            }
            else
            {
                sendingTask.TaskShouldWait();
                reciverTask.TaskShouldWait();
                connectionTask.TaskShouldContinue();    //if we don't have reconnecter-> task inside the handler is null,nothing happens
                communicationStream.Close();
            }
        }

        public void Dispose()
        {
            if (cts != null){ cts.Cancel(); }

            if (dataToSend != null) { dataToSend.Dispose();}

            if (communicationStream != null) { communicationStream.Dispose(); }

            if(sendingTask!=null) { sendingTask.DeleteTask();}

            if(reciverTask!=null) { reciverTask.DeleteTask();}

            if(connectionTask!=null) { connectionTask.DeleteTask();}
        }
    }
}
