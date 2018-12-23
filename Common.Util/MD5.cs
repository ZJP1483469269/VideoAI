using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace TLKJ.Utils
{
    public class MDUtil
    {
        /// <summary>
        /// 16位的MD5加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string Get16MD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(myString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="myString">要加密的密码</param>
        /// <returns></returns>
        public static string Get32MD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();  //实例化MD5对象
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)  // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="codeLength">加密长度</param>
        /// <returns></returns>
        public static string MD5(string password, int codeLength)
        {
            if (!string.IsNullOrEmpty(password))
            {
                // 16位MD5加密（取32位加密的9~25字符）  
                if (codeLength == 16)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower().Substring(8, 16);
                }

                // 32位加密
                if (codeLength == 32)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();
                }
            }
            return string.Empty;
        }


    }
}
