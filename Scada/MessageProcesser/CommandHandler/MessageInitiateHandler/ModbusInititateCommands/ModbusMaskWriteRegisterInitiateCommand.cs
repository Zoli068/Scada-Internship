using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusMaskWriteRegisterInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateMaskWriteModbusDTO DTO = messageDTO as InitiateMaskWriteModbusDTO;

            ModbusMaskWriteRegisterRequest req= new ModbusMaskWriteRegisterRequest();

            if (DTO.Address < 1)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            req.ReferenceAddress = DTO.Address;
            req.OrMask = DTO.OrMask;
            req.AndMask = DTO.AndMask;

            return req;
        }
    }
}
