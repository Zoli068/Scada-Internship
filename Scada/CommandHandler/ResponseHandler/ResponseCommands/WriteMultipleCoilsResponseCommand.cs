﻿using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.ResponseCommands
{
    public class WriteMultipleCoilsResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusWriteMultipleCoilsResponse res = response as ModbusWriteMultipleCoilsResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write Multiple Coils Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusWriteMultipleCoilsRequest req= request as ModbusWriteMultipleCoilsRequest;

            Console.WriteLine("Quantity of outputs:" + req.QuantityOfOutputs);

            byte temp;
            for (int i = 0; i < req.QuantityOfOutputs; i++)
            {
                Console.WriteLine("-------------------------------");
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((req.OutputsValue[byteIndex] & (1 << bitPosition)));
                if (temp != 0)
                    temp = 1;

                Console.WriteLine((req.StartingAddress + i) + " address:" + temp);
            }

            Console.WriteLine("-------------------------------");
        }
    }
}
