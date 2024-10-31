using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Utilities
{
    public class SerializationHandler
    {
        public SerializationHandler() {}

        /// <summary>
        /// Converts all the value type or value type array to a byte array from an <see cref="object"/>
        /// </summary>
        /// <param name="objToSerialize">Object which we want to serialize</param>
        /// <returns>The byte array from the type or value type array attributes</returns>
        public byte[] SerializeToBytes(object objToSerialize)
        {
            List<byte> byteList = new List<byte>();

            try
            {
                List<(Type, object)> attributeValueList = GetAttributes(objToSerialize);

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
            }catch(Exception ex)
            {
                if (ex is NotEnoughBytesException)
                {
                    throw ex;
                }
                else if (ex is UnsupportedTypeException)
                {   //this shouldn't happen here
                    throw ex;
                }
                else
                {
                    throw new SerializationException();
                }
            }

            return byteList.ToArray();
        }

        /// <summary>
        /// Creates a <paramref name="type"/> object, and the value type and value type arrays will be filled from the values from the byte[]<paramref name="data"/>
        /// </summary>
        /// <param name="data">The array which contains the values</param>
        /// <param name="offsetParam">Starting position inside the <see cref="data"/></param>
        /// <param name="type">The type of the object which we want to create</param>
        /// <returns></returns>
        public (object,int) DeserializeFromBytes(byte[] data,int offsetParam,Type type)
        {
            object obj;
            int offset;

            try
            {
                obj = Activator.CreateInstance(type);
                offset = offsetParam;
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
            }
            catch(Exception ex)
            {
                if(ex is NotEnoughBytesException)
                {
                    throw ex;
                }
                else if(ex is UnsupportedTypeException)
                {   //this shouldn't happen here
                    throw ex;
                }
                else
                {
                    throw new SerializationException();
                }
            }

            return (obj,offset);
        }

        /// <summary>
        /// Getting an <typeparamref name="T"/> type array from a byte array
        /// </summary>
        /// <typeparam name="T">Type of the array which we want to get</typeparam>
        /// <param name="data">The array to be extracted</param>
        /// <returns>The extracted array</returns>
        public T[] ExtractArray<T>(byte[] data) 
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

        /// <summary>
        /// Extracting bytes from an array with automatic byte order conversion
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="array">the array which we want to convert to byte array</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrives all the value type and value type array attributes from an object <paramref name="obj"/> 
        /// </summary>
        /// <returns>List with tuple elements, where we have the type of the attribute and the value of the attribute</returns>
        public List<(Type,object)> GetAttributes(object obj)
        {
            List<(Type, object)> typeValueList = new List<(Type, object)>();

            Type type = obj.GetType();

            try
            {
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
            }
            catch (Exception)
            {
                throw new NotSupportedException("Not supported object for this method");
            }

            return typeValueList;
        }

        /// <summary>
        /// Getting all the value type or value type array attributes inside a <paramref name="type"/> type object
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A List which holds all the value type or array from valuetype attributes</returns>
        public List<PropertyInfo> GetAttributes(Type type)
        {
            List<PropertyInfo> typeValueList = new List<PropertyInfo>();

            try
            {
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
            }
            catch (Exception)
            {
                throw new NotSupportedException("Not supported object for this method");
            }

            return typeValueList;
        }

        /// <summary>
        /// Converts the byte order of a byte array which does represents a <paramref name="type"/> variable
        /// </summary>
        /// <remarks>Works only with value types, for any other type will throw an exception</remarks>
        /// <param name="type">The type</param>
        /// <param name="data">The Byte array which will be converted</param>
        /// <returns></returns>
        public byte[] ConvertByteOrder(Type type, byte[] data)
        {
            try
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
                }
                else
                {
                    return data;//system is big endian->no need for convert
                }
            }
            catch(Exception)
            {
                throw new NotEnoughBytesException();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts a boxed value type variable to a byte array 
        /// </summary>
        /// <remarks>Works only with value types, for any other type will throw an exception</remarks>
        /// <param name="value">The value which we want to convert to byte[]</param>
        /// <returns>The byte[] value from the <paramref name="value"/></returns>
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

            throw new NotSupportedException();
        }

        /// <summary>
        /// From a sepicified <paramref name="offset"/> position inside the byte array <paramref name="data"/> creates a <paramref name="type"/> type variable 
        /// </summary>
        /// <remarks>Works only with value types, for any other type will throw an exception</remarks>
        /// <param name="data">Byte array from we will get our value</param>
        /// <param name="offset">Starting position for extracting the value inside the array</param>
        /// <param name="type">The specified type which one we want to extract</param>
        /// <returns>The extracted value and  the size of the value</returns>
        public (object value,int size) ConvertBytesToType(byte[] data, int offset, Type type)
        {
            try
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
            }
            catch (Exception) 
            { 
                throw new NotEnoughBytesException();
            }

            throw new NotSupportedException();
        }
    }
}

