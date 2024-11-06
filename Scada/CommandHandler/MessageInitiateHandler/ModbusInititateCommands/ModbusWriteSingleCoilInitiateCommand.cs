using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler.ModbusInititateCommands
{
    public class ModbusWriteSingleCoilInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusWriteSingleCoilRequest request = new ModbusWriteSingleCoilRequest();

            if (messageDTO.Address < 0)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            request.OutputAddress=messageDTO.Address;

            if (messageDTO.ByteValue == 0)
            {
                request.OutputValue = 0;
            }
            else
            {
                request.OutputValue = 0xFF00;
            }

            return request;
        }
    }
}
