using Common;
using Common.Message;
using Master.Communication;
using System;
using System.Net;
using System.Reflection;

namespace Master
{
    public class Scada
    {
        private CommunicationHandler communicationHandler;

        public Scada() { }

        public void Start() 
        {
            TcpCommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,2000,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000,SecurityMode.SECURE);
            CommunicationHandler communicationHandler=new CommunicationHandler(communicationHandlerOptions,tcpCommunicationOptions);   
        }
    }
}
