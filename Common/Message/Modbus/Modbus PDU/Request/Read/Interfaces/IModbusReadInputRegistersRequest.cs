using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadInputRegistersRequest
    {
        short StartingAddress { get; set; }
        short QuantityOfInputRegisters { get; set; }
    }
}
