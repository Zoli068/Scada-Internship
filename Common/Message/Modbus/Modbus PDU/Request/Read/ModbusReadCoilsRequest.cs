using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadCoilsRequest : IModbusReadCoilsRequest
    {
        private short startingAddress;
        private short quantityOfCoils;

        public ModbusReadCoilsRequest() { }

        public ModbusReadCoilsRequest(short startingAddress, short quantityOfCoils)
        {
            this.startingAddress = startingAddress;
            this.quantityOfCoils = quantityOfCoils;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfCoils, data, ref startIndex);
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

        public short QuantityOfCoils
        {
            get
            {
                return quantityOfCoils;
            }
            set
            {
                quantityOfCoils=value;
            }
        }

    }
}
