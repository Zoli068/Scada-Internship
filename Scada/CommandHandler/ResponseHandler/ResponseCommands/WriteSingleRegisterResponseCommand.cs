using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.ResponseCommands
{
    public class WriteSingleRegisterResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusWriteSingleRegisterResponse res = response as ModbusWriteSingleRegisterResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write Single Register Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusWriteSingleRegisterRequest req = request as ModbusWriteSingleRegisterRequest;

            Console.WriteLine(res.RegisterAddress + " address:" + res.RegisterValue);

            Console.WriteLine("-------------------------------");


        }
    }
}

