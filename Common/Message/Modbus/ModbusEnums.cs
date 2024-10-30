using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public enum FunctionCode : byte
    {
        ReadCoils = 0x01,
        ReadDiscreteInputs = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10,
    }

    public enum ExceptionCode : byte
    {
        IllegalFunction=0x01,
        IllegalDataAddress=0x02,
        IllegalDataValue=0x03,
        SlaveDeviceFailure=0x04,
    }
    
}
