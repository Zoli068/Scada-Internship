using Common;
using Common.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        public ICommunicationStream communicationStream;
        private IStateHandler<CommunicationState> stateHandler;
        private ICommunicationHandlerOptions options;
        private IAsyncSecureCommunication secureCommunication;

        private Task connectionAcceptingTask;
        private AutoResetEvent signaliseAccepting = new AutoResetEvent(false);

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions,ICommunicationOptions communicationOptions)
        {
            options= communicationHandlerOptions;
            stateHandler = new StateHandler<CommunicationState>();

            stateHandler.StateChanged += connectionStateChanged;

            if (options.SecurityMode == SecurityMode.SECURE)
            {
                secureCommunication=new SecureCommunication();
            }

            if (communicationOptions.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions);
            }

            taskInit();

            connectionAcceptingTask.Start();
            reciveTask.Start();
            sendingTask.Start();
        }
        
        public void taskInit()
        {
            connectionAcceptingTask = new Task(startAcceptingConnections);
        
            reciveTask= new Task(
                async () =>
                {
                    byte[] recivedData;

                    while (!endSignal)
                    {   
                        if(stateHandler.State != CommunicationState.CONNECTED)
                        {
                            signaliseReciving.WaitOne();
                        }

                        try
                        {
                            recivedData= await communicationStream.Receive();
                            Console.WriteLine("Master sent:");
                            Console.WriteLine(UnicodeEncoding.UTF8.GetString(recivedData));
                        }
                        catch(Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
                        {
                            Console.WriteLine(ex.Message);
                            stateHandler.ChangeState(CommunicationState.DISCONNECTED);
                        }
                    }
                });

            sendingTask = new Task(
                async () =>
                {
                    string input;

                    while (!endSignal)
                    {
                        if (stateHandler.State != CommunicationState.CONNECTED)
                        {
                            signaliseSending.WaitOne();
                        }

                        try
                        {
                            Console.WriteLine("Enter a string for sending");
                            input = Console.ReadLine();

                            if (input.Equals("exit"))
                            {
                                endSignal = true;
                                break;
                            }

                            await communicationStream.Send(UnicodeEncoding.UTF8.GetBytes(input));
                            Console.WriteLine("Message sent:" + input);

                        }
                        catch (Exception ex) when (ex is ConnectionErrorException || ex is ConnectionNotExisting)
                        {
                            Console.WriteLine(ex.Message);
                            stateHandler.ChangeState(CommunicationState.DISCONNECTED);
                        }
                    }

                });
        }

        private bool endSignal = false;
        private Task reciveTask;
        private Task sendingTask;
        private AutoResetEvent signaliseReciving = new AutoResetEvent(false);
        private AutoResetEvent signaliseSending = new AutoResetEvent(false);

        private async void startAcceptingConnections()
        {
            while (endSignal == false)
            {
                if (stateHandler.State == CommunicationState.CONNECTED)
                {
                    signaliseAccepting.WaitOne();
                }

                try
                {
                    await communicationStream.Accept();

                    if (options.SecurityMode == SecurityMode.SECURE)
                    {
                        communicationStream.Stream = await secureCommunication.SecureStream(communicationStream.Stream);
                    }
                
                    stateHandler.ChangeState(CommunicationState.CONNECTED);
                }
                catch(Exception ex) when (ex is UnsuccessfullConnectionException || ex is AuthenticationFailedException)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("waiting for a connection");
                    communicationStream.Close();
                }

            }
        }

        public void connectionStateChanged() {

            Console.WriteLine("Connection changed to: " + stateHandler.State);
            
            if(stateHandler.State== CommunicationState.CONNECTED)
            {
                signaliseSending.Set();
                signaliseReciving.Set();
            }
            else if(stateHandler.State==CommunicationState.DISCONNECTED)
            {
                signaliseReciving.Reset(); //this most important when we sending something bcs, we waiting for an input
                signaliseSending.Reset();//then we connected already for another client but already disconnected,
                                         //then a not used signal will be on the signaliseReciving,and have to be reseted.

                communicationStream.Close(); // if we want to drop the old connection when new
                                             // one come in that little modification have to do it
                signaliseAccepting.Set();
            }
        }
    }
}
