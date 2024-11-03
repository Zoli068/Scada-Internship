﻿using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusPDU : IModbusPDU
    {
        private FunctionCode functionCode;
        private IModbusData data;

        public ModbusPDU() { }

        public ModbusPDU(FunctionCode functionCode, IModbusData data)
        {
            this.functionCode = functionCode;
            this.data = data;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            functionCode = (FunctionCode)data[startIndex++];

            try
            {
                this.data = ModbusFunctionFactory.TypeMap[functionCode]();
                this.data.Deserialize(data, ref startIndex);
            }
            catch (Exception)
            {
                throw new NotSupportedException();
            }
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>() { (byte)functionCode };
            bytes.AddRange(data.Serialize());

            return bytes.ToArray();
        }

        public FunctionCode FunctionCode
        {
            get
            {
                return functionCode;
            }
            set
            {
                functionCode = value;
            }
        }

        public IModbusData Data{
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
    }
}
