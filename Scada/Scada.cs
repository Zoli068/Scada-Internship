using Common;
using Master.TcpCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Master
{
    public class Scada
    {
        private CommunicationHandler communicationHandler;

        public Scada() { }

        public void Start() {

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, SecurityMode.SECURE, 15,8000, 8192);

            CommunicationHandler communicationHandler=new CommunicationHandler(options);

            //this signal need inside the communicationHandler because we want a way to close the task.
            bool endSignal = false;

            //Our Thread inside the communicationHandler for reciving messages should be something like this in the end,
            //only diff that inside the communicationHandler at the continueWith part should trigger an ValueUpdate Event and pass the recived value
            Task reciveTestTask = new Task(
                async () =>
                {
                    while (!endSignal)
                    {
                        await communicationHandler.communicationStream.Receive().ContinueWith(t =>
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
                input=Console.ReadLine();

                if (input.Equals("exit"))
                {
                    endSignal = true;
                    break;
                }

                communicationHandler.communicationStream.Send(UnicodeEncoding.UTF8.GetBytes(input)).ContinueWith(t => {
                    Console.WriteLine("Message sent:"+input);
                });
            };
               
            Console.WriteLine("Exit the app");
            Console.ReadKey();
            communicationHandler.communicationStream.Disconnect();
        }

    }
}
