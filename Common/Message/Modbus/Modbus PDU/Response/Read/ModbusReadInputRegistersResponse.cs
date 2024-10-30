using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadInputRegistersResponse : IModbusReadInputRegistersResponse
    {
        private byte count;
        private short[] inputRegisers;

        public ModbusReadInputRegistersResponse() { }

        public ModbusReadInputRegistersResponse(byte count, short[] inputRegisers)
        {
            this.count = count;
            this.inputRegisers = inputRegisers;
        }

        [Order(1)]
        public byte Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        [Order(2)]
        public short[] InputRegisters
        {
            get
            {
                return inputRegisers;
            }
            set
            {
                inputRegisers = value;
            }
        }
    }
}
