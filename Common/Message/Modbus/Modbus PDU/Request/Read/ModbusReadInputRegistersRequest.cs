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
        private ushort startingAddress;
        private ushort quantityOfInputRegisters;

        public ModbusReadInputRegistersRequest() { }

        public ModbusReadInputRegistersRequest(ushort startingAddress, ushort quantityOfInputRegisters)
        {
            this.startingAddress=startingAddress;
            this.quantityOfInputRegisters=quantityOfInputRegisters;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfInputRegisters, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>(4);
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfInputRegisters));

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

        public ushort QuantityOfInputRegisters
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
