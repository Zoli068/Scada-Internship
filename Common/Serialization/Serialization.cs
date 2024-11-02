using Common.Message.Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Common.Serialization
{
    public static class Serialization
    {
        public static T CreateMessageObject<T>(byte[] data) where T: IDeserialize,new()
        {
            int parsedBytes = 0;

            IDeserialize deserializedObject = new T();
            deserializedObject.Deserialize(data,ref parsedBytes);

            return (T)deserializedObject;
        }

        public static byte[] ExtractMessageBytes<T>(T message) where T: ISerialize, new()
        {
            return message.Serialize();
        }

    }
}
