using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadCoilsRequest:IModbusData
    {
        ushort StartingAddress {get;set;}

        ushort QuantityOfCoils { get;set;}
    }
}
