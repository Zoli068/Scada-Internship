using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadHoldingRegistersResponse : IModbusData
    {
        byte ByteCount { get; set; }

        short[] RegisterValue { get; set; }
    }
}
