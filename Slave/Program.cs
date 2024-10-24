using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Common;
using Slave.Communication;

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(SecurityMode.SECURE);
            CommunicationHandler communicationHandler=new CommunicationHandler(communicationHandlerOptions,options);

            Task testTask = new Task(() => communicationHandler.TestingMethod());

            testTask.Start();
            Console.ReadKey();
        }
    }
}
