﻿using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.ResponseCommands
{
    public class ReadDiscreteResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadDiscreteInputsRequest req = (ModbusReadDiscreteInputsRequest)request;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read Discrete Inputs Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusReadDiscreteInputsResponse res= (ModbusReadDiscreteInputsResponse)response;

            Console.WriteLine("Quantity of inputs:" + req.QuantityOfInputs);

            byte temp;
            for (int i = 0; i < req.QuantityOfInputs; i++)
            {
                Console.WriteLine("-------------------------------");
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((res.InputStatus[byteIndex] & (1 << bitPosition)));
                if (temp != 0)
                    temp = 1;
                Console.WriteLine((req.StartingAddress + i) + " address:" + temp);
            }
            Console.WriteLine("-------------------------------");

        }
    }
}
