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
using System.Threading.Tasks;

namespace Master
{
    public class Scada
    {
        private ICommunication communication;
        private IMessageHandler messageHandler;
        private ModbusMessageInitiateHandler modbusMessageInitiateHandler;

        public Scada() { }

        public void Start() 
        {
            ICommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 502, CommunicationType.TCP, 2000, 8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000, SecurityMode.SECURE);
            communication=new Communication.Communication(tcpCommunicationOptions, communicationHandlerOptions);

            IResponseMessageDataHandler responseMessageDataHandler = new ModbusResponseMessageDataHandler();
            messageHandler = new TCPModbusMessageHandler(communication.SendBytes, responseMessageDataHandler);
            modbusMessageInitiateHandler = new ModbusMessageInitiateHandler(messageHandler.SendMessage);

            //UI Class


            communication.BytesRecived += messageHandler.ProcessBytes;

            Task.Run(() =>
                {
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadCoils, new ModbusReadCoilsRequest(1, 4)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadDiscreteInputs, new ModbusReadDiscreteInputsRequest(10001, 6)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadHoldingRegisters, new ModbusReadHoldingRegistersRequest(40001, 3)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadInputRegisters, new ModbusReadInputRegistersRequest(30001, 9)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.WriteSingleCoil, new ModbusWriteSingleCoilRequest(2, 0xFF00)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadCoils, new ModbusReadCoilsRequest(2, 1)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.WriteSingleRegister, new ModbusWriteSingleRegisterRequest(40001, 4333)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadHoldingRegisters, new ModbusReadHoldingRegistersRequest(40001, 1)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.WriteMultipleRegisters, new ModbusWriteMultipleRegistersRequest(40010, 3, 6, new short[3] { 766, 3434, 2343 })));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadHoldingRegisters, new ModbusReadHoldingRegistersRequest(40010, 3)));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.WriteMultipleCoils, new ModbusWriteMultipleCoilsRequest(1, 3, 1, new byte[1] { 3 })));
                    //Thread.Sleep(1000);
                    //messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadCoils, new ModbusReadCoilsRequest(1, 3)));                    
                    
                    //errorChecks
                    Thread.Sleep(1000);         //illegal function
                    messageHandler.SendMessage(new ModbusPDU((FunctionCode)0x39, new ModbusReadCoilsRequest(1, 3)));                    
                    
                    Thread.Sleep(1000);         //Invalid value
                    messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadCoils, new ModbusReadCoilsRequest(2, 20001)));     
                    
                    Thread.Sleep(1000);         //Invalid address
                    messageHandler.SendMessage(new ModbusPDU(FunctionCode.ReadCoils, new ModbusReadCoilsRequest(9999, 21)));         
                    
                    Thread.Sleep(1000);         //writing on  input registers
                    messageHandler.SendMessage(new ModbusPDU(FunctionCode.WriteSingleRegister, new ModbusWriteSingleRegisterRequest(30001, 4333)));

                });
        }
    }
}
