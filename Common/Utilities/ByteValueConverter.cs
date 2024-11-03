using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class ByteValueConverter
    {
        public static byte[] ExtractBytes(byte value)
        {
            return new byte[] { value };
        }

        public static byte[] ExtractBytes(short value)
        {
            return ConvertTheByteOrder(BitConverter.GetBytes(value));
        }

        public static byte[] ExtractBytes(ushort value)
        {
            return ConvertTheByteOrder(BitConverter.GetBytes(value));
        }

        public static byte[] ExtractBytes(int value)
        {
            return ConvertTheByteOrder(BitConverter.GetBytes(value));
        }

        public static byte[] ExtractBytes(uint value)
        {
            return ConvertTheByteOrder(BitConverter.GetBytes(value));
        }

        public static byte[] ExtractBytes(float value)
        {
            return ConvertTheByteOrder(BitConverter.GetBytes(value));
        }

        public static void GetValue(out byte value, byte[] bytes, ref int startIndex)
        {
            value = bytes[startIndex++];
        }

        public static void GetValue(out short value, byte[] bytes,ref int startIndex)
        {
            byte[] byteToConvert = new byte[2];
            Buffer.BlockCopy(bytes, startIndex, byteToConvert, 0, 2);
            byteToConvert = ConvertTheByteOrder(byteToConvert);
            value =BitConverter.ToInt16(byteToConvert, 0);
            startIndex += 2;
        }

        public static void GetValue(out ushort value, byte[] bytes,ref int startIndex)
        {
            byte[] byteToConvert = new byte[2];
            Buffer.BlockCopy(bytes, startIndex, byteToConvert, 0, 2);
            byteToConvert = ConvertTheByteOrder(byteToConvert);
            value =BitConverter.ToUInt16(byteToConvert, 0);
            startIndex += 2;
        }

        public static void GetValue(out int value, byte[] bytes,ref int startIndex)
        {
            byte[] byteToConvert = new byte[4];
            Buffer.BlockCopy(bytes, startIndex, byteToConvert, 0, 4);
            byteToConvert = ConvertTheByteOrder(byteToConvert);
            value =BitConverter.ToInt32(byteToConvert, 0);
            startIndex += 4;
        }

        public static void GetValue(out uint value, byte[] bytes,ref int startIndex)
        {
            byte[] byteToConvert = new byte[4];
            Buffer.BlockCopy(bytes, startIndex, byteToConvert, 0, 4);
            byteToConvert = ConvertTheByteOrder(byteToConvert);
            value =BitConverter.ToUInt32(byteToConvert, 0);
            startIndex += 4;
        }
        
        public static void GetValue(out float value, byte[] bytes,ref int startIndex)
        {
            byte[] byteToConvert = new byte[8];
            Buffer.BlockCopy(bytes,startIndex,byteToConvert,0,8);
            byteToConvert = ConvertTheByteOrder(byteToConvert);
            value=BitConverter.ToInt64(byteToConvert, 0);
            startIndex += 8;
        }

        private static byte[] ConvertTheByteOrder(byte[] byteArray)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteArray);
            }

            return byteArray;
        }
    }
}
