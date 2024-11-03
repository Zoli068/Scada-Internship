using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleCoilRequest : IModbusWriteSingleCoilRequest
    {
        private short outputAddress;
        private short outputValue;

        public ModbusWriteSingleCoilRequest() { }

        public ModbusWriteSingleCoilRequest(short outputAddress, short outputValue)
        {
            this.outputAddress = outputAddress;
            this.outputValue = outputValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out outputAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out outputValue, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.AddRange(ByteValueConverter.ExtractBytes(outputAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(outputValue));

            return data.ToArray();
        }

        public short OutputAddress
        {
            get
            {
                return outputAddress;
            }
            set
            {
                outputAddress = value;
            }
        }

        public short OutputValue
        {
            get
            {
                return outputValue;
            }
            set
            {
                outputValue = value;
            }
        }

    }
}
