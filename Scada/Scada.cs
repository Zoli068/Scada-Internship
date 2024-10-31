using Common;
using Common.Message;
using Common.Message.Modbus;
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
        private CommunicationHandler communicationHandler;

        public Scada() { }

        public void Start() 
        {
            TcpCommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,2000,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000,SecurityMode.SECURE,MessageType.TCPModbus);
            CommunicationHandler communicationHandler=new CommunicationHandler(communicationHandlerOptions,tcpCommunicationOptions);   

            ///TESTS
            SerializationHandler serializationHandler = new SerializationHandler();

            ModbusMessage modbusMessage = new ModbusMessage();
            modbusMessage.MessageHeader = new TCPModbusHeader(2, 2, 13, 1);
            modbusMessage.MessageData = new ModbusPDU(FunctionCode.WriteMultipleRegisters, null);
            (modbusMessage.MessageData as ModbusPDU).Data= new ModbusWriteMultipleRegistersRequest(12, 4, 3, new short[] { 45, 67, 89 });
            
            List<byte> tosendbytes = new List<byte>();

            tosendbytes.AddRange(serializationHandler.SerializeToBytes(modbusMessage.MessageHeader));
            tosendbytes.AddRange(serializationHandler.SerializeToBytes(modbusMessage.MessageData));
            tosendbytes.AddRange(serializationHandler.SerializeToBytes((modbusMessage.MessageData as ModbusPDU).Data));

            communicationHandler.dataToSend.Add(tosendbytes.ToArray());
            //Thread.Sleep(5000);
            tosendbytes[5] = 14;//check if the header length > actual length
            //communicationHandler.dataToSend.Add(tosendbytes.ToArray());
            //Thread.Sleep(10000);
            //tosendbytes[5] = 13;//check if the header length > actual length
            //tosendbytes[13] = 4;//check if the header count bigger then the available bytes
            //communicationHandler.dataToSend.Add(tosendbytes.ToArray());
            //Thread.Sleep(10000);
            //tosendbytes[13] = 3;
            //tosendbytes[7] = 77;    //not supported
            //communicationHandler.dataToSend.Add(tosendbytes.ToArray());
        }
    }
}
