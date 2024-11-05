using Common;
using Common.Command;
using Common.Communication;
using Common.ICommunication;
using Common.Message;
using Common.Message.Modbus;
using Common.Serialization;
using Common.Utilities;
using Master.CommandHandler;
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
        private IMessageHandler messageHandler;
        private IMessageDataHandler messageDataHandler;

        public Scada() { }

        public void Start() 
        {
            ICommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000, SecurityMode.SECURE);
            communication=new Communication.Communication(tcpCommunicationOptions, communicationHandlerOptions);

            messageHandler = new TCPModbusMessageHandler(communication.SendBytes);
            messageDataHandler = new ModbusResponseMessageDataHandler();

            communication.BytesRecived += messageHandler.ProcessBytes;

        }
    }
}
