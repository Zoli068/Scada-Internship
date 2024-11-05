using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.ResponseCommands
{
    public class ReadCoilsResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData data)
        {
            ModbusReadCoilsResponse response=data as ModbusReadCoilsResponse;
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Read Coils Response:");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("ByteCount:" + response.ByteCount);
            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.WriteLine("-------------------------------");
        }
    }
}
