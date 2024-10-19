using Master.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Net;
using System.IO.Ports;
using System.Net.Security;
using Master.TcpCommunication;

namespace Master
{
    public class Program
    {
        static void Main(string[] args)
        {

            TCPConfiguration tcpConfiguration = new TCPConfiguration(IPAddress.Parse("127.0.0.1"), 8000, CommunicationType.TCP, SecurityMode.INSECURE);
            CommunicationHandler communicationHandler = new CommunicationHandler(tcpConfiguration);

            communicationHandler.communicationStream.SendBytes(Encoding.UTF8.GetBytes("HELLO WORLD\n"));
        }
    }
}
