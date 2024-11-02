using Common;
using Common.Communication;
using Common.Message;
using Common.Message.Modbus;
using Common.Serialization;
using Common.Utilities;
using Master.Communication;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Master
{
    public class Scada
    {
        private ICommunication communication;
        
        public Scada() { }

        public void Start() 
        {
            TcpCommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000, SecurityMode.SECURE, MessageType.TCPModbus);
            communication=new Communication.Communication(tcpCommunicationOptions, communicationHandlerOptions);

            byte[] test = new byte[] { 2, 0, 2, 0, 13, 0, 1, 16, 56, 0, 67 };

            TCPModbusMessage message=Serialization.CreateMessageObject<TCPModbusMessage>(test);

        }
    }
}
