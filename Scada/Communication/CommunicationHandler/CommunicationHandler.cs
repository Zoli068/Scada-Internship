using Common;
using Common.CommunicationExceptions;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
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
        private TimedExecutionHandler timedExecutionHandler;
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
        }

        public async void connectTheStream()
        {
            try
            {
                await communicationStream.Connect();
            }catch (UnsuccessfullConnectionException uns_ex)
            {
                Console.WriteLine(uns_ex.Message);
            }catch ( ConnectionAlreadyExisting cae_ex )
            {
                Console.WriteLine(cae_ex.Message);
                //stopping the reconnection bcs we does have a connection
                timedExecutionHandler.StopTimer();
            }

            if (communicationStream.Stream != null)
            {
                if (options.SecurityMode == SecurityMode.SECURE)
                {
                    communicationStream.Stream = secureCommunication.SecureStream(communicationStream.Stream);
                    if (communicationStream.Stream == null)
                    {
                        //No Secure connection for u
                        communicationStream.Disconnect();
                    }
                }

                stateHandler.ChangeState(CommunicationState.CONNECTED);
            }
            else
            {
                stateHandler.ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);
            }
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

                bool endSignal = false;

                try
                {

                //Our Thread inside the communicationHandler for reciving messages should be something like this in the end,
                //only diff that inside the communicationHandler at the continueWith part should trigger an ValueUpdate Event and pass the recived value
                Task reciveTestTask = new Task(
                    async () =>
                    {
                        while (!endSignal)
                        {
                            await communicationStream.Receive().ContinueWith(t =>
                            {
                                if (t.Result.Length > 0)
                                {
                                    Console.Write("Got a message:");
                                    Console.WriteLine(UnicodeEncoding.UTF8.GetString(t.Result));
                                }
                            });
                        }
                    });

                reciveTestTask.Start();
                string input;

                //from 2 async method, but in the final version in the  communicationHandler, the writingTask, will try to dequeue a message from our Queue
                //and like that we won't have problem with that.

                //that also should be a thread in the communicationHandler, or an eventHandler
                //and we wouldn't use here direct input but the Queueu values from the communicationHandler.

                while (!endSignal)
                {
                    Console.WriteLine("Enter a string for sending");
                    input = Console.ReadLine();

                    if (input.Equals("exit"))
                    {
                        endSignal = true;
                        break;
                    }

                    communicationStream.Send(UnicodeEncoding.UTF8.GetBytes(input)).ContinueWith(t => {


                        Console.WriteLine("Message sent:" + input);
                    });
                };

                Console.WriteLine("Exit the app");
                Console.ReadKey();
                communicationStream.Disconnect();
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
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
