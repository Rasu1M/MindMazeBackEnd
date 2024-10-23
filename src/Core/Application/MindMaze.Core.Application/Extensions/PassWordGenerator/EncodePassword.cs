using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MindMaze.Core.Application.Extensions.PassWordGenerator
{
    public static class EncodePassword
    {
        public static string EncryptPassWord(string password,out byte[] passwordkey)
        {
            using var hmac = new HMACSHA512();

            passwordkey = hmac.Key;

           return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public static string EncryptPassWordWithKey(string password, byte[] key)
        {
            using var hmac = new HMACSHA512(key);

            return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public static string GetTokenID()
        {
            char[] Codes = new char[]
           {
                '1', '2', '3','4','5','6','7','8','9','0','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
           };

            var rand = new Random();

            return Codes[rand.Next(36)].ToString() + Codes[rand.Next(36)].ToString() + Codes[rand.Next(36)].ToString() + Codes[rand.Next(36)].ToString();


        }
    }
}
