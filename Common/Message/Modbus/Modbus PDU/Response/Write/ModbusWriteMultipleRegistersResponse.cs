using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteMultipleRegistersResponse : IModbusWriteMultipleRegistersResponse
    {
        private short startingAddress;
        private short quantityOfRegisters;

        public ModbusWriteMultipleRegistersResponse() { }

        public ModbusWriteMultipleRegistersResponse(short startingAddress, short quantityOfRegisters)
        {
            this.startingAddress = startingAddress;
            this.quantityOfRegisters = quantityOfRegisters;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfRegisters, data, ref startIndex);
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
    }
}
