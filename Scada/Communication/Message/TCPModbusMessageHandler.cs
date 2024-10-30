using Common;
using Common.Message;
using Common.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Communication
{
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private Queue<byte[]> byteMessageQueue;
        private AutoResetEvent messageAvailableForSend;
        private SerializationHandler serializationHandler;
        private ModbusFunctionDictionary modbusFunctionDictionary;

        private int currentTransactionID=1;
        private int unitIdentifier = 2;

        public TCPModbusMessageHandler()
        {
            byteMessageQueue= new Queue<byte[]>();
            messageAvailableForSend=new AutoResetEvent(false);
            serializationHandler= new SerializationHandler();
            modbusFunctionDictionary=new ModbusFunctionDictionary();
        }

        public void CreateMessageObject(byte[] data)
        {
            Type type = null;
            object temp = null;
            int readedBytes = 0;

            (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(TCPModbusHeader));
            TCPModbusHeader header = (TCPModbusHeader)temp;

            if((data.Length - header.Length) < 0)
            {
                //exception,we lost some bytes
            }

            (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(ModbusPDU));
            ModbusPDU modbusPDU = (ModbusPDU)temp;

            //Error!
            if (((byte)modbusPDU.FunctionCode & 0x80)!=0)
            {
                (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, typeof(ModbusError));
                modbusPDU.Data = temp as IModbusData;
            }
            else
            {       
                //ErrorCheck inside the functionCode
                if (modbusFunctionDictionary.ResponseTypeMap.TryGetValue(modbusPDU.FunctionCode, out type))
                {
                    (temp, readedBytes) = serializationHandler.DeserializeFromBytes(data, readedBytes, type);
                    modbusPDU.Data= temp as IModbusData;
                }
                else
                {
                    //exception non supported functionCode
                }
                
            }

            //TO CONTINUE


            //commandHandler call

             modbusPDU.Data = temp as IModbusData;
        }

        public void CreateByteArrayFromMessage(IMessage message)
        {
            throw new NotImplementedException();
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
