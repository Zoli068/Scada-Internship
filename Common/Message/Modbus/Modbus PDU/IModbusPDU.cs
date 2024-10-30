using Common.Message.Modbus.Request;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusPDU : IMessageData
    {
        FunctionCode FunctionCode { get; set; }

        IModbusData Data { get; set; }
    }
}
