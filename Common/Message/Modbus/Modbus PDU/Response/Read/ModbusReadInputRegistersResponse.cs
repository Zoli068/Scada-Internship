using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadInputRegistersResponse : IModbusReadInputRegistersResponse
    {
        private byte byteCount;
        private short[] inputRegisters;

        public ModbusReadInputRegistersResponse() { }

        public ModbusReadInputRegistersResponse(byte byteCount, short[] inputRegisers)
        {
            this.byteCount = byteCount;
            this.inputRegisters = inputRegisers;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            inputRegisters = new short[byteCount];

            for (int i = 0; i < byteCount / 2; i++)
            {
                ByteValueConverter.GetValue(out inputRegisters[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.Add(byteCount);

            for (int i = 0; i < byteCount / 2; i++)
            {
                data.AddRange(ByteValueConverter.ExtractBytes(InputRegisters[i]));
            }

            return data.ToArray();
        }

        public byte Count
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

        public short[] InputRegisters
        {
            get
            {
                return inputRegisters;
            }
            set
            {
                inputRegisters = value;
            }
        }
    }
}
