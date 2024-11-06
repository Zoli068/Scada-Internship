using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler.ModbusInititateCommands
{
    public class ModbusWriteSingleRegisterInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusWriteSingleRegisterRequest request = new ModbusWriteSingleRegisterRequest();

            if (messageDTO.Address < 0)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }
            request.RegisterAddress = messageDTO.Address;
            request.RegisterValue = messageDTO.ShortValue;

            return request;
        }
    }
}
