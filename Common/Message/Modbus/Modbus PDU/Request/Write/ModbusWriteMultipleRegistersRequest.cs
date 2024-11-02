﻿using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteMultipleRegistersRequest : IModbusWriteMultipleRegistersRequest
    {
        private short startingAddress;
        private short quantityOfRegisters;
        private short byteCount;
        private short[] registerValue;

        public ModbusWriteMultipleRegistersRequest() { }

        public ModbusWriteMultipleRegistersRequest(short startingAddress, short quantityOfRegisters, short byteCount, short[] registerValue)
        {
            this.startingAddress = startingAddress;
            this.quantityOfRegisters = quantityOfRegisters;
            this.byteCount = byteCount;
            this.registerValue = registerValue;

        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfRegisters, data, ref startIndex);
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            registerValue = new short[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                ByteValueConverter.GetValue(out registerValue[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public short StartingAddress
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

        public short QuantityOfRegisters
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

        public short ByteCount
        {
            get
            {
                return byteCount;
            }
            set
            {
                byteCount = value;
            }
        }

        public short[] RegisterValue
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