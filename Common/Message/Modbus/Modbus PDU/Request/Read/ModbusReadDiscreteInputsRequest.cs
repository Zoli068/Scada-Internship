using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadDiscreteInputsRequest : IModbusReadDiscreteInputsRequest
    {
        private ushort startingAddress;
        private ushort quantityOfInputs;

        public ModbusReadDiscreteInputsRequest() { }

        public ModbusReadDiscreteInputsRequest(ushort startingAddress, ushort quantityOfInputs)
        {
            this.startingAddress = startingAddress;
            this.quantityOfInputs = quantityOfInputs;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfInputs, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>(4);
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfInputs));

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

        public ushort QuantityOfInputs
        {
            get
            {
                return quantityOfInputs;
            }
            set
            {
                quantityOfInputs = value;
            }
        }
    }
}
