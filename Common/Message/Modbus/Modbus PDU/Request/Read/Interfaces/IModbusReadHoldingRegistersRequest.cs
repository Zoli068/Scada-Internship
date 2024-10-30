using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadHoldingRegistersRequest
    {
        short StartingAddress { get; set; }
        short QuantityOfRegisters { get; set; } 
    }
}
