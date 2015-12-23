using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.StockingBuffer
{
    class Program
    {
        static void Main(string[] args)
        {
            string secret = "ckczppom";

            int number = 0;

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            while(true)
            {
                string input = secret + number;
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                bool isResult = SixLeadingZeroes(hash);

                if (isResult)
                {
                    Console.WriteLine(number);
                }
                number++;
            }

            

            // step 2, convert byte array to hex string
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            





        }

        private static bool FiveLeadingZeroes(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().StartsWith("00000");
        }

        private static bool SixLeadingZeroes(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().StartsWith("000000");
        }
    }
}
