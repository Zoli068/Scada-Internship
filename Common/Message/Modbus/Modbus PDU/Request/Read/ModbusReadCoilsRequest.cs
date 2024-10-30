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
