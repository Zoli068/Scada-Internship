using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteMultipleCoilsResponse : IModbusWriteMultipleCoilsResponse
    {
        private short startingAddress;
        private short quantityOfOutputs;

        public ModbusWriteMultipleCoilsResponse() { }

        public ModbusWriteMultipleCoilsResponse(short startingAddress, short quantityOfOutputs)
        {
            this.startingAddress = startingAddress;
            this.quantityOfOutputs = quantityOfOutputs;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfOutputs, data, ref startIndex);
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

        public short QuantityOfOutputs
        {
            get
            {
                return quantityOfOutputs;
            }
            set
            {
                quantityOfOutputs = value;
            }
        }
    }
}
