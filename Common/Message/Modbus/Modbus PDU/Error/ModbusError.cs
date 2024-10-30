using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusError : IModbusError
    {
        private byte errorCode;
        private ExceptionCode exceptionCode;

        public ModbusError() { }
        public ModbusError( byte errorCode, ExceptionCode exceptionCode)
        {
            this.errorCode = errorCode;
            this.exceptionCode = exceptionCode;
        }

        [Order(1)]
        public byte ErrorCode
        {
            get
            {
                return errorCode;
            }
            set
            {
                errorCode = value;
            }
        }

        [Order(2)]
        public ExceptionCode ExceptionCode
        {
            get
            {
                return exceptionCode;
            }
            set
            {
                exceptionCode = value;
            }
        }
    }
}
