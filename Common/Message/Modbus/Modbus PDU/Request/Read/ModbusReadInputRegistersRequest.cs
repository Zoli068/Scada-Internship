using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadInputRegistersRequest:IModbusReadInputRegistersRequest
    {
        private short startingAddress;
        private short quantityOfInputRegisters;

        public ModbusReadInputRegistersRequest() { }

        public ModbusReadInputRegistersRequest(short startingAddress, short quantityOfInputRegisters)
        {
            this.startingAddress=startingAddress;
            this.quantityOfInputRegisters=quantityOfInputRegisters;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startIndex, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfInputRegisters, data, ref startIndex);
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

        public short QuantityOfInputRegisters
        {
            get
            {
                return quantityOfInputRegisters;
            }
            set
            {
                quantityOfInputRegisters = value;
            }
        }
    }
}
