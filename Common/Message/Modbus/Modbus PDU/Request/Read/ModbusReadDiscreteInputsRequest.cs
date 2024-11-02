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
        private short startingAddress;
        private short quantityOfInputs;

        public ModbusReadDiscreteInputsRequest() { }

        public ModbusReadDiscreteInputsRequest(short startingAddress, short quantityOfInputs)
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

        public short QuantityOfInputs
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
