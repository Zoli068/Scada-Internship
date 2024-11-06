using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.ResponseCommands
{
    public class WriteSingleCoilResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusWriteSingleCoilResponse res = response as ModbusWriteSingleCoilResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write Single Coil Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusWriteSingleCoilRequest req = request as ModbusWriteSingleCoilRequest;

            byte temp;
            if (req.OutputValue == 0)
            {
                temp = 0;
            }
            else
            {
                temp = 1;
            }

            Console.WriteLine(req.OutputAddress + " address: " + temp);
            Console.WriteLine("-------------------------------");
        }
    }
}
