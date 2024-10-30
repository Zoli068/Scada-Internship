using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleCoilResponse : IModbusWriteSingleCoilResponse
    {
        private short outputAddress;
        private short outputValue;
        
        public ModbusWriteSingleCoilResponse() { }

        public ModbusWriteSingleCoilResponse(short outputAddress, short outputValue)
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
                outputValue= value;
            }
        }
    }
}
