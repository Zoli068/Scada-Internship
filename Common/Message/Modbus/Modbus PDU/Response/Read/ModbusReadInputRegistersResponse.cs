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
        private byte count;
        private short[] inputRegisters;

        public ModbusReadInputRegistersResponse() { }

        public ModbusReadInputRegistersResponse(byte count, short[] inputRegisers)
        {
            this.count = count;
            this.inputRegisters = inputRegisers;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out count, data, ref startIndex);

            inputRegisters = new short[count];

            for (int i = 0; i < count; i++)
            {
                ByteValueConverter.GetValue(out inputRegisters[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public byte Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
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
