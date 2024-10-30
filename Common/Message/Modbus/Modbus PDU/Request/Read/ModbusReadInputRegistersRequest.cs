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
