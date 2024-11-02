using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Contains all the attributes for a ModbusError
    /// </summary>
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

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out errorCode,data,ref startIndex);
            exceptionCode =(ExceptionCode)data[startIndex++];
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

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
