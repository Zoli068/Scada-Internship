using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Common.ICommunication;
using Common.Message;
using Common.TaskHandler;
using System;
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
        private TaskHandler connectionTask;
        private ICommunicationStream communicationStream;
        private ICommunicationHandlerOptions options;
        private IAsyncSecureCommunication secureCommunication;
        private IStateHandler<CommunicationState> stateHandler;
        private IMessageHandler messageHandler;

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions, ICommunicationOptions communicationOptions)
        {
            this.options = communicationHandlerOptions;

            stateHandler = new StateHandler<CommunicationState>();
            stateHandler.StateChanged += connectionStateChanged;

            if (options.SecurityMode == SecurityMode.SECURE)
            {
                secureCommunication = new SecureCommunication();
            }

            if (communicationOptions.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions);
            }

            if (options.MessageType == MessageType.TCPModbus)
            {
                messageHandler = new TCPModbusMessageHandler();
            }

            if (options.ReconnectInterval > 0)
            {
                connectionTask = new TaskHandler(connectTheStream, false,options.ReconnectInterval);
            }
            else
            {
                connectionTask = new TaskHandler(connectTheStream, true, options.ReconnectInterval);
            }

            TaskInit();

            connectionTask.TaskShouldContinue();
        }

        private void TaskInit()
        {
            reciveTestTask = new Task(
                async () =>
                {
                    byte[] recivedData;

                    while (!endSignal)
                    {
                        if (stateHandler.State != CommunicationState.CONNECTED)
                        {
                            signaliseReciveTask.WaitOne();
                        }

                        try
                        {
                            recivedData = await communicationStream.Receive();

                            Console.Write("Slave sent a message:");
                            Console.WriteLine(UnicodeEncoding.UTF8.GetString(recivedData));

                        }
                        catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
                        {
                            Console.WriteLine(ex.Message);
                            stateHandler.ChangeState(CommunicationState.DISCONNECTED);
                        }
                    }
                });

            sendingTestTask = new Task(
                async () =>
                {
                    string input;
                    while (!endSignal)
                    {
                        if (stateHandler.State != CommunicationState.CONNECTED)
                        {
                            signaliseSendingTask.WaitOne();
                        }

                        Console.WriteLine("Enter a string for sending");
                        input = Console.ReadLine();

                        if (input.Equals("exit"))
                        {
                            endSignal = true;
                            break;
                        }

                        try
                        {
                            await communicationStream.Send(UnicodeEncoding.UTF8.GetBytes(input));

                            Console.WriteLine("Message sent:" + input);
                        }
                        catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
                        {
                            Console.WriteLine(ex.Message);
                            stateHandler.ChangeState(CommunicationState.DISCONNECTED);
                        }
                    };
                });
            reciveTestTask.Start();
            sendingTestTask.Start();
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

        //when implementing the communicationHandler in fully it will be changed
        //also will be implemented with a TaskHandler.
        private bool endSignal = false;
        private Task reciveTestTask;
        private Task sendingTestTask;
        private AutoResetEvent signaliseReciveTask=new AutoResetEvent(false);
        private AutoResetEvent signaliseSendingTask=new AutoResetEvent(false);

        private void connectionStateChanged()
        {
            Console.WriteLine("Changed state to " + stateHandler.State);
            if (stateHandler.State == CommunicationState.CONNECTED)
            {
                connectionTask.TaskShouldWait();
                signaliseReciveTask.Set();
                signaliseSendingTask.Set();
            }
            else if (stateHandler.State == CommunicationState.DISCONNECTED)
            {
                signaliseReciveTask.Reset();
                signaliseSendingTask.Reset();

                if (options.ReconnectInterval > 0) //if we got disconnected -> error while using the connection
                {                                  //or we manually disconnected then we want to reconnect
                    connectionTask.TaskShouldContinue();
                }
                communicationStream.Close(); //to be sure we closed everything properly
            }
            else
            {
                communicationStream.Close(); //to be sure we closed everything 
                connectionTask.TaskShouldContinue();
                signaliseReciveTask.Reset();
                signaliseSendingTask.Reset();
            }
        }
    }
}
