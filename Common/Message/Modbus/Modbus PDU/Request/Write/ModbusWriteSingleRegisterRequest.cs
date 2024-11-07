﻿using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusWriteSingleRegisterRequest"/> interface
    /// </summary>
    public class ModbusWriteSingleRegisterRequest : IModbusWriteSingleRegisterRequest
    {
        private ushort registerAddress;
        private short registerValue;

        public ModbusWriteSingleRegisterRequest() { }

        public ModbusWriteSingleRegisterRequest(ushort registerAddress, short registerValue)
        {
            this.registerAddress = registerAddress;
            this.registerValue = registerValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out registerAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out registerValue, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.AddRange(ByteValueConverter.ExtractBytes(registerAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(registerValue));

            return data.ToArray();
        }

        public ushort RegisterAddress
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
