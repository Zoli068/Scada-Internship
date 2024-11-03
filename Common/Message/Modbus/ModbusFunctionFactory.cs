using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public static class ModbusFunctionFactory
    {
        public static Dictionary<FunctionCode, Func<IModbusData>> TypeMap;

        static ModbusFunctionFactory()
        {
            string mode = ConfigurationManager.AppSettings["ModbusMode"];

            if(mode == "Slave")
            {
                TypeMap = new Dictionary<FunctionCode, Func<IModbusData>>()
                {
                    { FunctionCode.ReadCoils, () => new ModbusReadCoilsRequest() },
                    { FunctionCode.ReadDiscreteInputs, () => new ModbusReadDiscreteInputsRequest() },
                    { FunctionCode.ReadHoldingRegisters, () => new ModbusReadHoldingRegistersRequest() },
                    { FunctionCode.ReadInputRegisters, () => new ModbusReadInputRegistersRequest() },
                    { FunctionCode.WriteSingleCoil, () => new ModbusWriteSingleCoilRequest() },
                    { FunctionCode.WriteSingleRegister, () => new ModbusWriteSingleRegisterRequest() },
                    { FunctionCode.WriteMultipleCoils, () => new ModbusWriteMultipleCoilsRequest() },
                    { FunctionCode.WriteMultipleRegisters, () => new ModbusWriteMultipleRegistersRequest() }
                };
            }
            else
            {
                TypeMap = new Dictionary<FunctionCode, Func<IModbusData>>()
                {
                    { FunctionCode.ReadCoils, () => new ModbusReadCoilsResponse() },
                    { FunctionCode.ReadDiscreteInputs, () => new ModbusReadDiscreteInputsResponse() },
                    { FunctionCode.ReadHoldingRegisters, () => new ModbusReadHoldingRegistersResponse() },
                    { FunctionCode.ReadInputRegisters, () => new ModbusReadInputRegistersResponse() },
                    { FunctionCode.WriteSingleCoil, () => new ModbusWriteSingleCoilResponse() },
                    { FunctionCode.WriteSingleRegister, () => new ModbusWriteSingleRegisterResponse() },
                    { FunctionCode.WriteMultipleCoils, () => new ModbusWriteMultipleCoilsResponse() },
                    { FunctionCode.WriteMultipleRegisters, () => new ModbusWriteMultipleRegistersResponse() }
                };
            }
        }
    }
}
