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

        [Order(1)]
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

        [Order(2)]
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
