using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusRequestData : IModbusRequestData
    {
        private short startingAddress;
        private short quantity;

        public ModbusRequestData() { }

        public ModbusRequestData(short startingAddress, short quantity)
        {
            this.startingAddress= startingAddress;
            this.quantity= quantity;
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
        public short Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
    }
}
