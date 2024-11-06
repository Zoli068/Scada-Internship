using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusReadCoilsInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO,IModbusData>
    {
        IModbusData IMessageInitiateCommand<ModbusMessageDTO, IModbusData>.InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusReadCoilsRequest req=new ModbusReadCoilsRequest();

            if(messageDTO.Address<0 || messageDTO.Quantity < 0 || messageDTO.Quantity > 2000)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            req.StartingAddress = messageDTO.Address;
            req.QuantityOfCoils = messageDTO.Quantity;

            return req;
        }
    }
}
