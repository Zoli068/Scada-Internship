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

            TCPCommunicationOptions options = new TCPCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, SecurityMode.SECURE,11);
            TcpCommunicationStream stream = new TcpCommunicationStream(options);

            Console.WriteLine(Encoding.UTF8.GetString(stream.RecvBytes()));
            Console.ReadKey();
        }
    }
}
