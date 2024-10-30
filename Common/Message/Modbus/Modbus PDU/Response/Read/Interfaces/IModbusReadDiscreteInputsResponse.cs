using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadDiscreteInputsResponse: IModbusData
    {
        byte ByteCount { get; set; }
        byte[] InputStatus { get; set; }
    }
}
