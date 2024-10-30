using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusFunctionDictionary
    {
        public Dictionary<FunctionCode, Type> ResponseTypeMap = new Dictionary<FunctionCode, Type>()  
        {
            { FunctionCode.ReadCoils, typeof(ModbusReadCoilsResponse) },
            { FunctionCode.ReadDiscreteInputs, typeof(ModbusReadDiscreteInputsResponse) },
            { FunctionCode.ReadHoldingRegisters, typeof(ModbusReadHoldingRegistersResponse) },
            { FunctionCode.ReadInputRegisters, typeof(ModbusReadInputRegistersResponse) },
            { FunctionCode.WriteSingleCoil, typeof(ModbusWriteSingleCoilResponse) },
            { FunctionCode.WriteSingleRegister, typeof(ModbusWriteSingleRegisterResponse) },
            { FunctionCode.WriteMultipleCoils, typeof(ModbusWriteMultipleCoilsResponse) },
            { FunctionCode.WriteMultipleRegisters, typeof(ModbusWriteMultipleRegistersResponse) }
        };

        public Dictionary<FunctionCode, Type> RequestTypeMap = new Dictionary<FunctionCode, Type>()
        {
            { FunctionCode.ReadCoils, typeof(ModbusReadCoilsRequest) },
            { FunctionCode.ReadDiscreteInputs, typeof(ModbusReadDiscreteInputsRequest) },
            { FunctionCode.ReadHoldingRegisters, typeof(ModbusReadHoldingRegistersRequest) },
            { FunctionCode.ReadInputRegisters, typeof(ModbusReadInputRegistersRequest) },
            { FunctionCode.WriteSingleCoil, typeof(ModbusWriteSingleCoilRequest) },
            { FunctionCode.WriteSingleRegister, typeof(ModbusWriteSingleRegisterRequest) },
            { FunctionCode.WriteMultipleCoils, typeof(ModbusWriteMultipleCoilsRequest) },
            { FunctionCode.WriteMultipleRegisters, typeof(ModbusWriteMultipleRegistersRequest) }
        };
    }
}
