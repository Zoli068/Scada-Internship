using Common;
using Common.ICommunication;
using Master.Communication;
using Master.TcpCommunication;
using Scada;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        public ICommunicationStream communicationStream;
        private TimedExecutionHandler timedExecutionHandler;

        public CommunicationHandler(ICommunicationOptions options)
        {
            timedExecutionHandler = new TimedExecutionHandler((options as ITcpCommunicationOptions).ReconnectInterval);

            if (options.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(options as ITcpCommunicationOptions);

                communicationStream.StateChanged += stateGotChangedSoDoSomething;
            }

            //Autorecconect or not
            if (true)
            {
                timedExecutionHandler.timer.Elapsed += (sender, e) => communicationStream.Connect();
                timedExecutionHandler.StartTimer();
            }
        
        }

        //Thread.Sleep(Timeout.Infinite) to put to sleep the task
        //when we lose the connection..
        private void stateGotChangedSoDoSomething()
        {
            Console.WriteLine("Changed state to " + communicationStream.State);
            if (communicationStream.State == CommunicationState.CONNECTED)
            {
                timedExecutionHandler.StopTimer();
                //this signal need inside the communicationHandler because we want a way to close the task.
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

                //for not having bugs right now, we have to wait little bit between sending the message (Maybe not sure but i think its a possibility),
                //not accidentally accesss the write stream
                //from 2 async method, but in the final version in the  communicationHandler, the writingTask, will try to dequeue a message from our Queue
                //and like that we won't have problem with that.


                //that also should be a thread in the communicationHandler, or an eventHandler
                //and we wouldn't use here direct input but the Queueu values from the communicationHandler.
                //if we doing with thread then checking for state, if not then inside an event handler we sending the messages
                //maybe both of them, like with eventhandler we wake up the thread for sending bytes

                //and like here we can reuse the IAbbstractStateHandler, because that class just offering us that stuff.
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

                timedExecutionHandler.StopTimer();
                timedExecutionHandler.StartTimer();
            }
            //else stopping those task
        }
    }
}
