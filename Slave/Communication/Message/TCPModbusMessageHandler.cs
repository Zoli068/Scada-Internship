using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slave.Communication.Message
{
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private Queue<byte[]> byteMessageQueue;
        private AutoResetEvent messageAvailableForSend;
        private SerializationHandler serializationHandler;
        private ModbusFunctionDictionary modbusFunctionDictionary;

        private int currentTransactionID = 1;
        private int unitIdentifier = 2;

        public TCPModbusMessageHandler()
        {
            byteMessageQueue = new Queue<byte[]>();
            messageAvailableForSend = new AutoResetEvent(false);
            serializationHandler = new SerializationHandler();
            modbusFunctionDictionary = new ModbusFunctionDictionary();
        }

        public void CreateMessageObject(byte[] data)
        {
            Type type = null;
            object temp = null;
            int readedBytes = 0;

            (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(TCPModbusHeader));
            TCPModbusHeader header = (TCPModbusHeader)temp;

            if ((data.Length - header.Length) < 0)
            {
                //exception,we lost some bytes
            }

            (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(ModbusPDU));
            ModbusPDU modbusPDU = (ModbusPDU)temp;

            if (modbusFunctionDictionary.ResponseTypeMap.TryGetValue(modbusPDU.FunctionCode, out type))
            {
                (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, type);
            }
            else
            {
                //exception non supported functionCode
            }
        }

        public void CreateByteArrayFromMessage(IMessage message)
        {
            int offset = 0;
            byte[] data = new byte[260];//max size is 260 for modbus PDU
            byte[] serializedObject=null;

            TCPModbusHeader header= (TCPModbusHeader)message.MessageHeader;
            ModbusPDU modbusPDU= message.MessageData as ModbusPDU;
            IModbusData modbusData=modbusPDU.Data;

            serializedObject = serializationHandler.SerializeToBytes(header);
            Buffer.BlockCopy(serializedObject,0,data, offset, serializedObject.Length);
            offset += serializedObject.Length;

            serializedObject = serializationHandler.SerializeToBytes(modbusPDU);
            Buffer.BlockCopy(serializedObject, 0, data, offset, serializedObject.Length);
            offset += serializedObject.Length;

            serializedObject = serializationHandler.SerializeToBytes(modbusData);
            Buffer.BlockCopy(serializedObject, 0, data, offset, serializedObject.Length);
            offset += serializedObject.Length;



        }

        public Queue<byte[]> ByteMessagesQueue
        {
            get
            {
                return byteMessageQueue;
            }
        }

        public AutoResetEvent MessageAvailableForSend
        {
            get
            {
                return messageAvailableForSend;
            }
        }
    }
}
