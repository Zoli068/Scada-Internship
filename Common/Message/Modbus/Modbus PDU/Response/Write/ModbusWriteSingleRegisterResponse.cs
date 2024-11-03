using Common.Utilities;
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
