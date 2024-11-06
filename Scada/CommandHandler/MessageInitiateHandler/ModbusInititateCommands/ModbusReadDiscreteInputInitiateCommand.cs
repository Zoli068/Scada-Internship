using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusReadDiscreteInputInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusReadDiscreteInputsRequest req=new ModbusReadDiscreteInputsRequest();

            if (messageDTO.Address < 0 || messageDTO.Quantity < 0 || messageDTO.Quantity > 2000)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            req.StartingAddress = messageDTO.Address;
            req.QuantityOfInputs = messageDTO.Quantity;

            return req;
        }
    }
}
