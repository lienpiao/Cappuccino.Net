using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.Common.Util
{
    /// <summary>
    /// MD5加密工具类
    /// </summary>
    public class Md5Utils
    {
        /// <summary>
        /// 加密位数16
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string EncryptTo16(string str)
        {

            string tmp = EncryptTo32(str).Substring(8, 16);
            return tmp.ToString().Substring(8, 16);
        }

        /// <summary>
        /// 加密位数32
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string EncryptTo32(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(str));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
    }
}
