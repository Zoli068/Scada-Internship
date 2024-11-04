using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteMultipleCoilsResponse : IModbusData
    {
        ushort StartingAddress { get; set; }
        ushort QuantityOfOutputs { get; set; }
    }
}
