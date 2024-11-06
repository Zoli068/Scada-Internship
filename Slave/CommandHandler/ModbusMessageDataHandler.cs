using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.Message.Modbus;
using Common.PointsDataBase;
using Slave.CommandHandler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Slave.CommandHandler
{

    public class ModbusMessageDataHandler: IMessageDataHandler
    {
        private readonly Dictionary<FunctionCode,IMessageDataCommand<IModbusData>> commands;
        
        public ModbusMessageDataHandler(IPointsDataBase pointsDataBase)
        {
            commands = new Dictionary<FunctionCode, IMessageDataCommand<IModbusData>> ()
            {
                {FunctionCode.ReadCoils , new ReadCoilsCommand(pointsDataBase)},
                {FunctionCode.ReadDiscreteInputs, new ReadDiscreteInputsCommand(pointsDataBase)},
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersCommand(pointsDataBase)},
                {FunctionCode.ReadInputRegisters, new ReadInputRegistersCommand(pointsDataBase)},
                {FunctionCode.WriteMultipleCoils, new WriteMultipleCoilsCommand(pointsDataBase)},
                {FunctionCode.WriteMultipleRegisters, new WriteMultipleRegistersCommand(pointsDataBase)},
                {FunctionCode.WriteSingleCoil, new WriteSingleCoilCommand(pointsDataBase)},
                {FunctionCode.WriteSingleRegister, new WriteSingleRegisterCommand(pointsDataBase)},
            };
        }
        
        public IMessageData ProcessMessageData(IMessageData data)
        {
            IMessageDataCommand<IModbusData> command;

            if(commands.TryGetValue(((IModbusPDU)data).FunctionCode,out command)){

                try
                {
                    return CreateMessageData(command.Execute(((IModbusPDU)data).Data), ((IModbusPDU)data).FunctionCode);
                }
                catch(ValueOutOfIntervalException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode,ExceptionCode.IllegalDataValue,3);
                }
                catch(InvalidAddressException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.IllegalDataAddress,2);
                }
                catch (PointTypeDifferenceException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.SlaveDeviceFailure,4);
                }
            }
            else
            {
                return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.IllegalFunction,1);
            }
        }

        private IMessageData CreateMessageData(IModbusData modbusData,FunctionCode functionCode)
        {
             ModbusPDU modbusPDU = new ModbusPDU();
             modbusPDU.FunctionCode = functionCode;
             modbusPDU.Data = modbusData;

            return modbusPDU;
        }

        private IMessageData CreateErrorMessage(FunctionCode code,ExceptionCode exceptionCode,byte errorCode)
        {
            byte errorFunctionCode = (byte)(((byte)code) + 0x80);
            ModbusPDU modbusPDU = new ModbusPDU();

            modbusPDU.FunctionCode = (FunctionCode)errorFunctionCode;                                                       //ERRORCODE
            modbusPDU.Data= new ModbusError(errorCode, exceptionCode);      //should go the error code

            return modbusPDU;
        }
    }
}
