using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadHoldingRegistersResponse : IModbusReadHoldingRegistersResponse
    {
        private byte byteCount;
        private short[] registerValue;

        public ModbusReadHoldingRegistersResponse() { }

        public ModbusReadHoldingRegistersResponse(byte byteCount, short[] registerValue)
        {
            this.byteCount = byteCount;
            this.registerValue = registerValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
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

        public byte ByteCount
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
