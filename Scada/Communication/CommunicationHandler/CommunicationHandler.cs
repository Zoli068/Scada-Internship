using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Common.ICommunication;
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
        private Task reconnectTask;
        private AutoResetEvent signaliseReconnect= new AutoResetEvent(false);
        public ICommunicationStream communicationStream;
        private ICommunicationHandlerOptions options;
        private ISecureCommunication secureCommunication;
        private IStateHandler<CommunicationState> stateHandler;

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

            taskInit();
        }
        private void taskInit()
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
            reconnectTask = new Task(connectTheStream);

            reconnectTask.Start();
            reciveTestTask.Start();
            sendingTestTask.Start();
        }

        //maybe creating an own class?
        public async void connectTheStream()
        {
            while (endSignal == false) //maybe no need for that, we can maybe just abort the task, and no need here for that
            { 
                try
                {
                    await communicationStream.Connect();

                    if (options.SecurityMode == SecurityMode.SECURE)
                    {
                        communicationStream.Stream = secureCommunication.SecureStream(communicationStream.Stream);
                    }

                    stateHandler.ChangeState(CommunicationState.CONNECTED);
                    signaliseReconnect.WaitOne();
                }
                catch (Exception ex) when (ex is UnsuccessfullConnectionException || ex is ConnectionAlreadyExisting || ex is AuthenticationFailedException)
                {
                    Console.WriteLine(ex.Message);    //exceptions we expecting to happen sometimes in the communication
                    stateHandler.ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);

                    if (options.ReconnectInterval > 0)
                    {
                        Thread.Sleep(options.ReconnectInterval);
                    }
                    else
                    {
                        break; 
                    }
                }

            } 
        }

        //when implementing the communicationHandler in fully it will be changed
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
                signaliseReciveTask.Set();
                signaliseSendingTask.Set();
            }
            else if (stateHandler.State == CommunicationState.DISCONNECTED)
            {
                signaliseReciveTask.Reset();
                signaliseSendingTask.Reset();

                if (options.ReconnectInterval > 0) //if we got disconnected -> error while using the connection
                {                                  //or we manually disconnected then we want to reconnect
                    signaliseReconnect.Set();
                }
                communicationStream.Close(); //to be sure we closed everything properly
            }
            else
            {
                signaliseReciveTask.Reset();
                signaliseSendingTask.Reset();
            }
        }
    }
}
