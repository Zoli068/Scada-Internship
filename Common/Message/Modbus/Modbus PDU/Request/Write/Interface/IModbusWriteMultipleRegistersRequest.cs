using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteMultipleRegistersRequest : IModbusData
    {
        short StartingAddress { get; set; }
        short QuantityOfRegisters {  get; set; }    
        short ByteCount {  get; set; }
        short[] RegisterValue { get; set; }
    }
}
