﻿using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadHoldingRegistersRequest : IModbusReadHoldingRegistersRequest
    {
        private ushort startingAddress;
        private ushort quantityOfRegisters;

        public ModbusReadHoldingRegistersRequest() { }

        public ModbusReadHoldingRegistersRequest(ushort startingAddress, ushort quantityOfRegisters)
        {
            this.startingAddress = startingAddress;
            this.quantityOfRegisters = quantityOfRegisters;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfRegisters, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>(4);
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfRegisters));

            return data.ToArray();
        }

        public ushort StartingAddress
        {
            get
            {
                return startingAddress;
            }
            set
            {
                startingAddress = value;
            }
        }

        public ushort QuantityOfRegisters
        {
            get
            {
                return quantityOfRegisters;
            }
            set
            {
                quantityOfRegisters = value;
            }
        }
    }
}
