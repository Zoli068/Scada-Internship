using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler
{
    public class ModbusMessageDataHandler : IMessageDataHandler
    {
        private readonly Dictionary<FunctionCode, IMessageDataCommand<IModbusData>> commands;

        public ModbusMessageDataHandler()
        {
            commands = new Dictionary<FunctionCode, IMessageDataCommand<IModbusData>>()
            {

                //commands!

            };
        }

        public IMessageData ProcessMessageData(IMessageData data)
        {
            IMessageDataCommand<IModbusData> command;

            if (commands.TryGetValue(((IModbusPDU)data).FunctionCode, out command))
            {

                //command.Execute(((IModbusPDU)data).Data);
                //inside the command check for value is inside okey value-> if not then ExceptionCode3
                //check for address -> if not then Exception 02
                //cant write/ read then -> exceptionCode 04
            }
            else
            {
                ///not suported, creating the error Exception code 01
            }

            return null;
        }
    }
}
