using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleCoilRequest : IModbusWriteSingleCoilRequest
    {
        private short outputAddress;
        private short outputValue;

        public ModbusWriteSingleCoilRequest() { }

        public ModbusWriteSingleCoilRequest(short outputAddress, short outputValue)
        {
            this.outputAddress = outputAddress;
            this.outputValue = outputValue;
        }

        [Order(1)]
        public short OutputAddress
        {
            get
            {
                return outputAddress;
            }
            set
            {
                outputAddress = value;
            }
        }

        [Order(2)]
        public short OutputValue
        {
            get
            {
                return outputValue;
            }
            set
            {
                outputValue = value;
            }
        }
    }
}
