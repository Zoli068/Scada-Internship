using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Common;
using Slave.Communication;
using Slave.Communication.TCPCommunication;

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, SecurityMode.SECURE, 8192);
            TcpCommunicationStream stream = new TcpCommunicationStream(options);

            stream.Listening();


            bool endSignal = false;

            Task reciveTestTask = new Task(
                async () =>
                {
                    while (!endSignal)
                    {
                        await stream.Receive().ContinueWith(t =>
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

                stream.Send(UnicodeEncoding.UTF8.GetBytes(input)).ContinueWith(t => {
                    Console.WriteLine("Message sent:" + input);
                });
            };

            Console.WriteLine("Exit the app");
            Console.ReadKey();
            stream.Disconnect();
        }
    }
}
