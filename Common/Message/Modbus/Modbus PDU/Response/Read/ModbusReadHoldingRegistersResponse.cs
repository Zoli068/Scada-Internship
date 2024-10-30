using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadHoldingRegistersResponse : IModbusReadHoldingRegistersResponse
    {
        private byte byteCount;
        private short[] registerValue;

        public ModbusReadHoldingRegistersResponse() { }

        public ModbusReadHoldingRegistersResponse(byte byteCount, short[] registerValue)
        {
            this.byteCount = byteCount;
            this.registerValue = registerValue;
        }

        [Order(1)]
        public byte ByteCount
        {
            get
            {
                return byteCount;
            }
            set
            {
                byteCount = value;
            }
        }

        [Order(2)]
        public short[] RegisterValue
        {
            get
            {
                return registerValue;
            }
            set
            {
                registerValue = value;
            }
        }
    }
}
