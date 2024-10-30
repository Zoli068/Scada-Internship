using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadHoldingRegistersRequest : IModbusReadHoldingRegistersRequest
    {
        private short startingAddress;
        private short quantityOfRegisters;

        public ModbusReadHoldingRegistersRequest() { }

        public ModbusReadHoldingRegistersRequest(short startingAddress, short quantityOfRegisters)
        {
            this.startingAddress = startingAddress;
            this.quantityOfRegisters = quantityOfRegisters;
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
