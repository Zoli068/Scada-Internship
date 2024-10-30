using Common.Message.Modbus.Request;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusResponseData : IModbusResponseData
    {
        private byte count;
        private byte[] registerValues;

        public ModbusResponseData() { }

        public ModbusResponseData(byte count, byte[] registerValues)
        {
            this.count = count;
            this.registerValues = registerValues;
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
        public byte[] RegisterValues
        {
            get
            {
                return registerValues;
            }
            set
            {
                registerValues = value;
            }
        }
    }
}
