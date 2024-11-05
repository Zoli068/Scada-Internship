﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteSingleCoilRequest : IModbusData
    {
        ushort OutputAddress { get; set; }
        ushort OutputValue { get; set; } 
    }
}
