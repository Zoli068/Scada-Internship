﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteSingleCoilResponse : IModbusData
    {
        short OutputAddress { get; set; }
        short OutputValue { get; set; }
    }
}