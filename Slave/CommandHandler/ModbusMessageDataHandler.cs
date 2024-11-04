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
        IPointsDataBase pointsDataBase;
        private readonly Dictionary<FunctionCode,IMessageDataCommand<IModbusData>> commands;
        
        public ModbusMessageDataHandler(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
            commands = new Dictionary<FunctionCode, IMessageDataCommand<IModbusData>> ()
            {
                {FunctionCode.ReadCoils , new ReadCoilsCommand(pointsDataBase)},
                {FunctionCode.ReadDiscreteInputs, new ReadDiscreteInputsCommand(pointsDataBase)},
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersCommand(pointsDataBase)},
            };

        }
        
        public IMessageData ProcessMessageData(IMessageData data)
        {
            IMessageDataCommand<IModbusData> command;

            if(commands.TryGetValue(((IModbusPDU)data).FunctionCode,out command)){

                try
                {
                   return command.Execute(((IModbusPDU)data).Data) as IMessageData;
                }
                catch(ValueOutOfIntervalException)
                {
                    //creating response Error message with Exceptioncode2
                }
                catch(InvalidAddressException)
                {
                    //check for address -> if not then Exception 02
                }
                catch (PointTypeDifferenceException)
                {
                    //we tried to use 1 kind of pointType function on another type of pointType
                }
            }
            else
            {
                ///not suported, creating the error Exception code 01
            }

            return null;
        }
    }
}
