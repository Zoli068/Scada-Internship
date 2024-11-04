﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message 
{ 
    public interface IModbusWriteMultipleRegistersResponse : IModbusData
    {
        ushort StartingAddress { get; set; }
        ushort QuantityOfRegisters {  get; set; }
    }
}
