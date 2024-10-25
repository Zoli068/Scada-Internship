using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.CommunicationExceptions;
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
        public ICommunicationStream communicationStream;
        private ICommunicationHandlerOptions options;
        private ISecureCommunication secureCommunication=null;
        private TimedExecutionHandler timedExecutionHandler=null;
        private IStateHandler<CommunicationState> stateHandler;

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions, ICommunicationOptions communicationOptions)
        {
            this.options = communicationHandlerOptions;

            stateHandler = new StateHandler<CommunicationState>();
            stateHandler.StateChanged += connectionStateChanged;

            if (options.ReconnectInterval > 0)
            {
                timedExecutionHandler = new TimedExecutionHandler(options.ReconnectInterval); //when we parsing the options have to be carefull about timeout<interval only if the interval is not 0
                timedExecutionHandler.timer.Elapsed += (sender, e) => connectTheStream();
            }

            if (options.SecurityMode == SecurityMode.SECURE)
            {
                secureCommunication = new SecureCommunication();
            }

            if (communicationOptions.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions);
            }


            //TODO delete it that call
            testEnvironmentSetUp();
        }

        public async void connectTheStream()
        {
            try
            {
                await communicationStream.Connect();

                if (options.SecurityMode == SecurityMode.SECURE)
                {
                    communicationStream.Stream = secureCommunication.SecureStream(communicationStream.Stream);
                }

                stateHandler.ChangeState(CommunicationState.CONNECTED);
            }
            catch(Exception ex) when(ex is UnsuccessfullConnectionException || ex is ConnectionAlreadyExisting || ex is AuthenticationFailedException)
            {
                Console.WriteLine(ex.Message);    //exceptions we expecting to happen sometimes in the communication
                stateHandler.ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);
            }

        }

        //this part for testing out the methods we got
        //just temporarily
        private bool endSignal = false;
        private Task reciveTestTask;
        private Task sendingTestTask;
        private readonly SemaphoreSlim _SemaphoreStopTheReciveTask = new SemaphoreSlim(0,1);
        private readonly SemaphoreSlim _SemaphoreStopTheSendingTask = new SemaphoreSlim(0,1);

        private void testEnvironmentSetUp()
        {
            reciveTestTask= new Task(
                    async () =>
                    {
                        byte[] recivedData;

                        while (!endSignal)
                        {
                            if (stateHandler.State != CommunicationState.CONNECTED)
                            {
                                await _SemaphoreStopTheReciveTask.WaitAsync();
                            }

                            try
                            {

                                recivedData = await communicationStream.Receive();

                                Console.Write("Got a message:");
                                Console.WriteLine(UnicodeEncoding.UTF8.GetString(recivedData));

                            }
                            catch (Exception ex) when(ex is ConnectionErrorException || ex is ConnectionNotExisting)
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
                            await _SemaphoreStopTheSendingTask.WaitAsync();
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
                            communicationStream.Send(UnicodeEncoding.UTF8.GetBytes(input));

                            Console.WriteLine("Message sent:" + input);
                        }
                        catch(Exception ex) when(ex is ConnectionErrorException || ex is ConnectionNotExisting)
                        {
                            Console.WriteLine(ex.Message);
                            stateHandler.ChangeState(CommunicationState.DISCONNECTED);
                        }
                    };
                });
        }

        //Thread.Sleep(Timeout.Infinite) to put to sleep the task
        //when we lose the connection..
        private void connectionStateChanged()
        {
            Console.WriteLine("Changed state to " + stateHandler.State);
            if (stateHandler.State == CommunicationState.CONNECTED)
            {
                if (timedExecutionHandler!=null)
                {
                    timedExecutionHandler.StopTimer();
                }

                reciveTestTask.Start();
                sendingTestTask.Start();

                _SemaphoreStopTheReciveTask.Release();
                _SemaphoreStopTheSendingTask.Release();
            }
            else
            {
                if (timedExecutionHandler!=null)
                {
                    timedExecutionHandler.StartTimer();
                }
            }
        }
    }
}
