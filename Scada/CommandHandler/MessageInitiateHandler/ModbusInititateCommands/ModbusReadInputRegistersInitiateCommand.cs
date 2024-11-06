using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusReadInputRegistersInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusReadInputRegistersRequest req=new ModbusReadInputRegistersRequest();

            if (messageDTO.Address < 0 || messageDTO.Quantity < 0 || messageDTO.Quantity > 125)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            req.StartingAddress = messageDTO.Address;
            req.QuantityOfInputRegisters = messageDTO.Quantity;

            return req;
        }
    }
}
