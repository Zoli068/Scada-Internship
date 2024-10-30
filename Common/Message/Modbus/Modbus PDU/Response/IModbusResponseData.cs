using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus.Request
{
    public interface IModbusResponseData:IModbusData
    {
        byte Count { get; set; }

        byte [] RegisterValues { get; set; }
    }
}
