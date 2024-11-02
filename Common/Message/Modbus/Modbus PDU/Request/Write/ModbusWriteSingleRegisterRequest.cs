﻿using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleRegisterRequest : IModbusWriteSingleRegisterRequest
    {
        private short registerAddress;
        private short registerValue;

        public ModbusWriteSingleRegisterRequest() { }

        public ModbusWriteSingleRegisterRequest(short registerAddress, short registerValue)
        {
            this.registerAddress = registerAddress;
            this.registerValue = registerValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out registerAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out registerValue,data, ref startIndex);
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

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