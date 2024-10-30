﻿using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleRegisterResponse : IModbusWriteSingleRegisterResponse
    {
        private short registerAddress;
        private short registerValue;

        public ModbusWriteSingleRegisterResponse() { }

        public ModbusWriteSingleRegisterResponse(short registerAddress, short registerValue)
        {
            this.registerAddress = registerAddress;
            this.registerValue = registerValue;
        }

        [Order(1)]
        public short RegisterAddress
        {
            get
            {
                return registerAddress;
            }
            set
            {
                registerAddress = value;
            }
        }

        [Order(2)]
        public short RegisterValue
        {
            get
            {
                return registerValue;
            }
            set
            {
                registerValue = value;
            }
        }
    }
}
