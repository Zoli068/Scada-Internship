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
            //comunicationStack
            ICommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP, 2000, 8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000, SecurityMode.SECURE);
            communication=new Communication.Communication(tcpCommunicationOptions, communicationHandlerOptions);
            //

            messageHandler = new TCPModbusMessageHandler(communication.SendBytes);
            messageDataHandler = new ModbusMessageDataHandler();

            communication.BytesRecived += messageHandler.ProcessBytes;

            //byte[] test = new byte[] { 2, 0, 2, 0, 13, 0, 1, 16, 56, 0, 67 ,0};

            //ModbusMessage message=Serialization.CreateMessageObject<ModbusMessage>(test);
            ////working

            //message = new ModbusMessage(new ModbusPDU(FunctionCode.WriteMultipleCoils,new ModbusWriteMultipleCoilsRequest(3,1,2,new byte[2] { 1, 2 })), new TCPModbusHeader(2, 0, 8, 2));

            //messageHandler.SendMessage(message);
        }
    }
}
