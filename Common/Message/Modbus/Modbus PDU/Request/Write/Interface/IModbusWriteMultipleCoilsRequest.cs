using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteMultipleCoilsRequest
    {
        short StartingAddress { get; set; }
        short QuantityOfOutputs { get; set; }
        short ByteCount { get; set; }
        short[] OutputsValue { get; set; }
    }
}
