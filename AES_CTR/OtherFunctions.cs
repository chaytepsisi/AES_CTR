using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_CTR
{
    internal class OtherFunctions
    {
        public static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        public static byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        public static byte[] BinaryStringToByteArray(string input)
        {
            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            return bytes;
        }
        public static string ByteArrayToBinaryString(byte[] ba)
        {
            StringBuilder result = new StringBuilder();
            foreach (byte b in ba)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }
        public static byte[] IncrementArray(byte[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == 255)
                    arr[i] = 0;
                else
                {
                    arr[i]++;
                    break;
                }
            }
            return arr;

        }
    }
}
