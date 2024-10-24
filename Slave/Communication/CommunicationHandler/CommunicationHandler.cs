using Common;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private ISecureCommunication secureCommunication;

        public CommunicationHandler(ICommunicationHandlerOptions communicationHandlerOptions,ICommunicationOptions communicationOptions)
        {
            options= communicationHandlerOptions;
            stateHandler = new StateHandler<CommunicationState>();

            if (options.SecurityMode == SecurityMode.SECURE)
            {
                secureCommunication=new SecureCommunication();
            }

            if (communicationOptions.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(communicationOptions as ITcpCommunicationOptions);
            }
        }

        public async void TestingMethod()
        {
            await communicationStream.Accept();

            if (communicationStream.Stream != null)
            {
                stateHandler.ChangeState(CommunicationState.CONNECTED);
            }
            else
            {
                return;
            }

            if(options.SecurityMode == SecurityMode.SECURE)
            {
                communicationStream.Stream=secureCommunication.SecureStream(communicationStream.Stream);
            }


            bool endSignal = false;

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
        }
    }
}
