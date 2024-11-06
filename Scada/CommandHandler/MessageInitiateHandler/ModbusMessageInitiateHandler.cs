using Common.Message;
using Common.Message.Modbus;
using Common.Utilities;
using Master.CommandHandler.MessageInitiateHandler;
using Master.CommandHandler.MessageInitiateHandler.ModbusInititateCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler
{
    public class ModbusMessageInitiateHandler
    {
        private readonly Dictionary<FunctionCode, IMessageInitiateCommand<ModbusMessageDTO, IModbusData>> messageInitiateCommands;
        private Action<IMessageData> sendMessage;

        public ModbusMessageInitiateHandler(Action<IMessageData> sendMessage)
        {
            messageInitiateCommands = new Dictionary<FunctionCode, IMessageInitiateCommand<ModbusMessageDTO, IModbusData>>() {
                {FunctionCode.ReadCoils,new ModbusReadCoilsInitiateCommand() },
                {FunctionCode.ReadDiscreteInputs,new ModbusReadDiscreteInputInitiateCommand() },
                {FunctionCode.ReadHoldingRegisters,new ModbusReadHoldingRegistersInitiateCommand()},
                {FunctionCode.ReadInputRegisters,new ModbusReadInputRegistersInitiateCommand() },
                {FunctionCode.WriteMultipleCoils,new ModbusWriteMultipleCoilsInitiateCommand()},
                {FunctionCode.WriteMultipleRegisters,new ModbusWriteMultipleRegistersInitiateCommand()},
                {FunctionCode.WriteSingleCoil,new ModbusWriteSingleCoilInitiateCommand()},
                {FunctionCode.WriteSingleRegister,new ModbusWriteSingleRegisterInitiateCommand()},  
            };
            this.sendMessage = sendMessage;
        }

        public void InitiateMessage(FunctionCode code,ModbusMessageDTO modbusMessageDTO)
        {
            IMessageInitiateCommand<ModbusMessageDTO, IModbusData> message;
            IModbusData temp;

            if(messageInitiateCommands.TryGetValue(code, out message))
            {
                temp= message.InitiateMessage(modbusMessageDTO);
                ModbusPDU modbusPDU= new ModbusPDU();
                modbusPDU.FunctionCode = code;
                modbusPDU.Data = temp;
                sendMessage(modbusPDU);
            }
        }
    }
}
