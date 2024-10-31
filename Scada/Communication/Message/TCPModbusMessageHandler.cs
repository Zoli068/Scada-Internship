using Common;
using Common.Message;
using Common.Message.Exceptions;
using Common.Message.Modbus;
using Common.Utilities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Communication
{
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private BlockingCollection<byte[]> dataToSend;
        private SerializationHandler serializationHandler;
        private ModbusFunctionDictionary modbusFunctionDictionary;

        private int currentTransactionID=1;
        private int unitIdentifier = 2;

        public TCPModbusMessageHandler(bool requestHandler, BlockingCollection<byte[]> byteMessageQueue)
        {
            this.dataToSend = byteMessageQueue;
            serializationHandler = new SerializationHandler();
            modbusFunctionDictionary=new ModbusFunctionDictionary(requestHandler);
        }

        public void CreateMessageObject(byte[] data)
        {
            Type type = null;
            object temp = null;
            int readedBytes = 0;
            ModbusMessage modbusMessage = new ModbusMessage();

            try
            {
                (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(TCPModbusHeader));
                modbusMessage.MessageHeader = (TCPModbusHeader)temp;

                if ((data.Length - ((TCPModbusHeader)modbusMessage.MessageHeader).Length) < 7)
                {
                    throw new MissingBytesFromMessageException();
                }
                else if((data.Length - ((TCPModbusHeader)modbusMessage.MessageHeader).Length) > 7)
                {
                    throw new ByteSizeDoesntMatchTheLengthException();
                }

                (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(ModbusPDU));
                modbusMessage.MessageData = (ModbusPDU)temp;

                if (((byte)(modbusMessage.MessageData as ModbusPDU).FunctionCode & 0x80)!=0)
                {
                    (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(ModbusError));
                    (modbusMessage.MessageData as ModbusPDU).Data = temp as IModbusData;
                }
                else
                {       
                    if (modbusFunctionDictionary.TypeMap.TryGetValue((modbusMessage.MessageData as ModbusPDU).FunctionCode, out type))
                    {
                        (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, type);
                        (modbusMessage.MessageData as ModbusPDU).Data = temp as IModbusData;
                    }
                    else
                    {
                        throw new NotSupportedMessageException();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //commandHandler errorhandling
            }
        }

        public void CreateByteArrayFromMessage(IMessage message)
        {
            byte[] dataPart;
            Type type= message.GetType();
            List<byte> data = new List<byte>(260);

            try
            {
                //TcpModbusHeader
                dataPart = serializationHandler.SerializeToBytes(message.MessageHeader);
                data.AddRange(dataPart);

                //ModbusPDU,functioncode will be parsed here bcs modbusData will be not parsed here
                dataPart=serializationHandler.SerializeToBytes(message.MessageData);
                data.AddRange(dataPart);

                //ModbusData
                dataPart = serializationHandler.SerializeToBytes((message.MessageData as IModbusPDU).Data);
                data.AddRange(dataPart);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            dataToSend.Add(data.ToArray());   
        }
    }
}
