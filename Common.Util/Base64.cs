using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TLKJ.Utils
{
    public class Base64
    {
        public static String StrToBase64(String cStr)
        {
            System.Text.Encoding vEncode = System.Text.UTF8Encoding.Default;
            byte[] vByte = vEncode.GetBytes(cStr);
            return Convert.ToBase64String(vByte, 0, vByte.Length);
        }


        public static String Base64ToStr(String cStr)
        {
            byte[] vByte = Convert.FromBase64String(cStr);
            return UTF8Encoding.Default.GetString(vByte);
        }

        public static String LoadFileASBase64(String cFileName)
        {
            byte[] vByte = null;
            if (File.Exists(cFileName))
            {
                FileStream fsm = null;
                try
                {
                    fsm = new FileStream(cFileName, FileMode.Open);
                    vByte = new byte[fsm.Length];
                    //调用read读取方法  
                    fsm.Read(vByte, 0, vByte.Length);
                }
                finally
                {
                    if (fsm != null)
                    {
                        fsm.Close();
                    }
                    fsm = null;
                }
            }
            if (vByte != null)
            {
                return Convert.ToBase64String(vByte, 0, vByte.Length);
            }
            else
            {
                return null;
            }
        }

        public static bool SaveBase64File(String cFileName, String cFileContent)
        {
            byte[] vByte = Convert.FromBase64String(cFileContent);
            if (File.Exists(cFileName))
            {
                File.Delete(cFileName);
            }

            try
            {
                FileStream fsm = new FileStream(cFileName, FileMode.Create);
                //调用read读取方法  
                fsm.Write(vByte, 0, vByte.Length);
                fsm.Flush();
                fsm.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
