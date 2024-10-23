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

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, SecurityMode.SECURE,5000,8000, 8192);

            CommunicationHandler communicationHandler=new CommunicationHandler(options);

            
        }

    }
}
