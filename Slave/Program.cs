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

            Console.WriteLine("Enter to close");
            Console.ReadKey();

        }
    }
}
