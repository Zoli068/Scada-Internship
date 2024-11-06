﻿using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Master.CommandHandler.ResponseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler
{
    public class ModbusResponseMessageDataHandler : IResponseMessageDataHandler
    {
        private readonly Dictionary<FunctionCode, IResponseMessageDataCommand<IModbusData>> commands;

        public ModbusResponseMessageDataHandler()
        {
            commands = new Dictionary<FunctionCode, IResponseMessageDataCommand<IModbusData>>()
            {
                {FunctionCode.ReadCoils,new ReadCoilsResponseCommand() },
                {FunctionCode.ReadDiscreteInputs,new ReadDiscreteResponseCommand() },
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersResponseCommand() },
                {FunctionCode.ReadInputRegisters,new ReadInputRegistersResponseCommand() },
                {FunctionCode.WriteMultipleCoils,new WriteMultipleCoilsResponseCommand() },
                {FunctionCode.WriteMultipleRegisters,new WriteMultipleRegistersResponseCommand() },
                {FunctionCode.WriteSingleCoil, new WriteSingleCoilResponseCommand() },
                {FunctionCode.WriteSingleRegister,new WriteSingleRegisterResponseCommand() },
            };
        }

        public void ProcessMessageData(IMessageData request, IMessageData response)
        {
            IResponseMessageDataCommand<IModbusData> command;

            if(((byte)(((IModbusPDU)response).FunctionCode) & 0x80) == 0)
            {
                if (commands.TryGetValue(((IModbusPDU)response).FunctionCode , out command))
                {
                    command.Execute(((IModbusPDU)request).Data, ((IModbusPDU)response).Data);
                }
            }
            else
            {
                HandleError(response);
            }
        }

        private void HandleError(IMessageData response)
        {
            ModbusPDU errorPdu = response as ModbusPDU;
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Error Happened:");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Function code error:" + errorPdu.FunctionCode);
            Console.WriteLine("Error Code:" + ((ModbusError)errorPdu.Data).ErrorCode);
            Console.WriteLine("Exception Code:" + ((ModbusError)errorPdu.Data).ExceptionCode);
            Console.WriteLine("--------------------------------------------------------------");
        }
    }
}