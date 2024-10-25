using Common;
using Master.Communication;
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
            TcpCommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,2000,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000,SecurityMode.SECURE);
            CommunicationHandler communicationHandler=new CommunicationHandler(communicationHandlerOptions,tcpCommunicationOptions);  

            communicationHandler.connectTheStream();
        }
    }
}
