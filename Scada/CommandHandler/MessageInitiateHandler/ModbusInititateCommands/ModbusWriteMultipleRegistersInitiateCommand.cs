using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusWriteMultipleRegistersInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusWriteMultipleRegistersRequest request = new ModbusWriteMultipleRegistersRequest();

            if (messageDTO.Address < 0 || messageDTO.Quantity < 0 || messageDTO.Quantity > 123 || messageDTO.ShortArray.Length < messageDTO.Quantity)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            request.StartingAddress = messageDTO.Address;
            request.QuantityOfRegisters = messageDTO.Quantity;
            request.ByteCount = (byte)(request.QuantityOfRegisters * 2);
            request.RegisterValue =new short[request.ByteCount];

            for(int i=0; i<request.ByteCount; i++)
            {
                request.RegisterValue[i] = messageDTO.ShortArray[i];
            }

            return request;
        }
    }
}
