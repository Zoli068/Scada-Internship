using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteSingleRegisterResponse : IModbusData
    {
        ushort RegisterAddress { get; set; }
        short RegisterValue { get; set; }
    }
}
