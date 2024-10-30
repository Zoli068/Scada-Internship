using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadCoilsResponse : IModbusReadCoilsResponse
    {
        private byte byteCount;
        private byte[] coilStatus;

        public ModbusReadCoilsResponse() { }

        public ModbusReadCoilsResponse(byte byteCount, byte[] coilStatus)
        {
            this.byteCount = byteCount;
            this.coilStatus = coilStatus;
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
        public byte[] CoilStatus
        {
            get
            {
                return coilStatus;
            }
            set
            {
                coilStatus = value;
            }
        }
    }
}
