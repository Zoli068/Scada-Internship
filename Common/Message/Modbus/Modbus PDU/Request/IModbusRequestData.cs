using Common.Message.Modbus.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusRequestData: IModbusData
    {
        short StartingAddress { get; set; }

        short Quantity {  get; set; }
    }
}
