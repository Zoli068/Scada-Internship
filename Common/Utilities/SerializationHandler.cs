using Common.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class SerializationHandler
    {
        public SerializationHandler() {}

        public byte[] SerializeToBytes(object objToSerialize)
        {
            List<(Type, object)> attributeValueList = GetAttributes(objToSerialize);
            List<byte> byteList = new List<byte>();

            foreach ((Type type,object obj) in attributeValueList)
            {
                if (!type.IsArray)
                {
                    if (type == typeof(string))
                    {
                        foreach(Char c in ((string)obj).ToCharArray())
                        {
                            byteList.Add(Convert.ToByte(c));  
                        }
                    }

                    byteList.AddRange(ConvertByteOrder(type, GetBytesWithUnboxing(obj)));
                }
                else
                {
                    Array array = (Array)obj;

                    for(int i=0;i<array.Length; i++)
                    {
                        byteList.AddRange(ConvertByteOrder(type.GetElementType(), GetBytesWithUnboxing(array.GetValue(i))));
                    }
                }
            }

            return byteList.ToArray();
        }

        public (object,int) DeserializeFromBytes(byte[] data,int offsetParam,Type type) 
        { 
            object obj = Activator.CreateInstance(type);
            int offset = offsetParam;
            long lastValue = 0;

            foreach( PropertyInfo propertyInfo in GetAttributes(type))
            {
                Type propType=propertyInfo.PropertyType;

                if (propType.IsEnum)
                {
                    propType = Enum.GetUnderlyingType(propertyInfo.PropertyType);
                }

                if (propType.IsArray)
                {
                    propType = propertyInfo.PropertyType.GetElementType();

                    Array array = Array.CreateInstance(propType, lastValue);
                    
                    for(int i=0;i<array.Length; i++)
                    {
                        (object value,int size)=ConvertBytesToType(data, offset, propType);
                        offset += size;
                        array.SetValue(value, i);   
                    }

                    propertyInfo.SetValue(obj, array);
                }
                else
                {
                    (object value, int size) = ConvertBytesToType(data,offset, propType);

                    propertyInfo.SetValue(obj, value);

                    lastValue = Convert.ToInt64(value);

                    offset += size;
                }
            }

            return (obj,offset);
        }

        public T[] ExtractArray<T>(byte[] data) //check out for buggs
        {
            Type type = typeof(T);
            List<T> arrayList = new List<T>();

            for(int i = 0; i < data.Length;)
            {
                (object obj, int size) = ConvertBytesToType(data, i, type);
                arrayList.Add((T)obj);
                i += size;
            }

            return arrayList.ToArray();
        } 

        public byte[] ExtractBytes<T>(T[] array)
        {
            byte[] byteArray = new byte[0];
            byte[] extractedBytes = null;

            for(int i=0;i < array.Length; i++)
            {
                extractedBytes=GetBytesWithUnboxing(array[i]);
                extractedBytes=ConvertByteOrder(typeof(T), extractedBytes);
                byteArray=byteArray.Concat(extractedBytes).ToArray();
            }

            return byteArray;
        }

        public List<(Type,object)> GetAttributes(object obj)
        {
            List<(Type, object)> typeValueList = new List<(Type, object)>();

            Type type = obj.GetType();

            foreach (PropertyInfo property in type.GetProperties().OrderBy(p => p.GetCustomAttribute<OrderAttribute>()?.Order ?? int.MaxValue).ToList())
            {
                Type propertyType = property.PropertyType;
                var value = property.GetValue(obj);

                if (propertyType.IsEnum)
                {
                    propertyType = Enum.GetUnderlyingType(propertyType);
                    value = Convert.ChangeType(value, propertyType);
                }

                if (!propertyType.IsValueType)
                {
                    if (!propertyType.IsArray)
                    {
                        continue;
                    }
                }

                typeValueList.Add((propertyType, value));
            }

            return typeValueList;
        }

        public List<PropertyInfo> GetAttributes(Type type)
        {
            List<PropertyInfo> typeValueList = new List<PropertyInfo>();

            foreach (PropertyInfo property in type.GetProperties().OrderBy(p => p.GetCustomAttribute<OrderAttribute>()?.Order ?? int.MaxValue).ToList())
            {
                Type propertyType = property.PropertyType;

                if (!property.PropertyType.IsValueType)
                {
                    if (!propertyType.IsArray)
                    {
                        continue;
                    }
                }

                typeValueList.Add(property);
            }

            return typeValueList;
        }

        public byte[] ConvertByteOrder(Type type, byte[] data)
        {
            if (BitConverter.IsLittleEndian)
            {
                if (type == typeof(byte) || type == typeof(sbyte) || type == typeof(char))
                {
                    return data;
                }

                if (data.Length == 2)
                {
                    if (type == typeof(short))
                    {
                        Array.Reverse(data, 0, sizeof(short));
                        return data;
                    }
                    else if (type == typeof(ushort))
                    {
                        Array.Reverse(data, 0, sizeof(short));
                        return data;
                    }
                }

                if (data.Length == 4)
                {
                    if (type == typeof(int))
                    {
                        Array.Reverse(data, 0, sizeof(int));
                        return data;
                    }
                    else if (type == typeof(uint))
                    {
                        Array.Reverse(data, 0, sizeof(uint));
                        return data;
                    }
                    else if (type == typeof(float))
                    {
                        Array.Reverse(data, 0, sizeof(float));
                        return data;
                    }
                }

                if (data.Length == 8)
                {
                    if (type == typeof(long))
                    {
                        Array.Reverse(data, 0, sizeof(long));
                        return data;
                    }
                    else if (type == typeof(ulong))
                    {
                        Array.Reverse(data, 0, sizeof(ulong));
                        return data;
                    }
                    else if (type == typeof(double))
                    {
                        Array.Reverse(data, 0, sizeof(double));
                        return data;
                    }
                }
                //exception throw, for unsuported value
                return null;
            }
            else
            {
                return data;//system is big endian->no need for convert
            }
        }

        public byte[] GetBytesWithUnboxing(dynamic value)
        {
            if (value is int) return BitConverter.GetBytes((int)value);
            if (value is short) return BitConverter.GetBytes((short)value);
            if (value is long) return BitConverter.GetBytes((long)value);
            if (value is float) return BitConverter.GetBytes((float)value);
            if (value is double) return BitConverter.GetBytes((double)value);
            if (value is bool) return BitConverter.GetBytes((bool)value);
            if (value is char) return BitConverter.GetBytes((char)value);
            if (value is ushort) return BitConverter.GetBytes((ushort)value);
            if (value is uint) return BitConverter.GetBytes((uint)value);
            if (value is ulong) return BitConverter.GetBytes((ulong)value);
            if (value is byte) return new byte[] { (byte)value };

            return null; //exception
        }

        public (object value,int size) ConvertBytesToType(byte[] data, int offset, Type type)
        {
            if (type == typeof(int))
            {
                byte[] intBytes = new byte[sizeof(int)];
                Array.Copy(data, offset, intBytes, 0, sizeof(int));

                byte[] convertedBytes = ConvertByteOrder(typeof(int), intBytes);
                int value = BitConverter.ToInt32(convertedBytes, 0);

                return (value, sizeof(int));
            }
            if (type == typeof(uint))
            {
                byte[] uintBytes = new byte[sizeof(uint)];
                Array.Copy(data, offset, uintBytes, 0, sizeof(uint));

                byte[] convertedBytes = ConvertByteOrder(typeof(uint), uintBytes);
                uint value = BitConverter.ToUInt32(convertedBytes, 0);

                return (value, sizeof(uint));
            }
            if (type == typeof(short))
            {
                byte[] shortBytes = new byte[sizeof(short)];
                Array.Copy(data, offset, shortBytes, 0, sizeof(short));

                byte[] convertedBytes = ConvertByteOrder(typeof(short), shortBytes);
                short value = BitConverter.ToInt16(convertedBytes, 0);

                return (value, sizeof(short));
            }
            if (type == typeof(ushort))
            {
                byte[] ushortBytes = new byte[sizeof(ushort)];
                Array.Copy(data, offset, ushortBytes, 0, sizeof(ushort));

                byte[] convertedBytes = ConvertByteOrder(typeof(ushort), ushortBytes);
                ushort value = BitConverter.ToUInt16(convertedBytes, 0);

                return (value, sizeof(ushort));
            }
            if (type == typeof(long))
            {
                byte[] longBytes = new byte[sizeof(long)];
                Array.Copy(data, offset, longBytes, 0, sizeof(long));

                byte[] convertedBytes = ConvertByteOrder(typeof(long), longBytes);
                long value = BitConverter.ToInt64(convertedBytes, 0);

                return (value, sizeof(long));
            }
            if (type == typeof(ulong))
            {
                byte[] ulongBytes = new byte[sizeof(ulong)];
                Array.Copy(data, offset, ulongBytes, 0, sizeof(ulong));

                byte[] convertedBytes = ConvertByteOrder(typeof(ulong), ulongBytes);
                ulong value = BitConverter.ToUInt64(convertedBytes, 0);

                return (value, sizeof(ulong));
            }
            if (type == typeof(float))
            {
                byte[] floatBytes = new byte[sizeof(float)];
                Array.Copy(data, offset, floatBytes, 0, sizeof(float));

                byte[] convertedBytes = ConvertByteOrder(typeof(float), floatBytes);
                float value = BitConverter.ToSingle(convertedBytes, 0);

                return (value, sizeof(float));
            }
            if (type == typeof(double))
            {
                byte[] doubleBytes = new byte[sizeof(double)];
                Array.Copy(data, offset, doubleBytes, 0, sizeof(double));

                byte[] convertedBytes = ConvertByteOrder(typeof(double), doubleBytes);
                double value = BitConverter.ToDouble(convertedBytes, 0);

                return (value, sizeof(double));
            }
            if (type == typeof(bool))
            {
                bool value = BitConverter.ToBoolean(data, offset);
                return (value, sizeof(bool));
            }
            if (type == typeof(char))
            {
                char value = BitConverter.ToChar(data, offset);
                return (value, sizeof(char));
            }
            if (type == typeof(bool))
            {
                bool value = BitConverter.ToBoolean(data, offset);
                return (value, sizeof(bool));
            }
            if (type == typeof(byte))
            {
                byte value = data[offset];
                return (value, sizeof(byte));
            }

            //Exception!
            return (null,0);
        }
    }
}

