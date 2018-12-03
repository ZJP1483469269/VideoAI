using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
/// <summary>
/// AppManager 的摘要说明
/// </summary>
namespace TLKJ.Utils
{
    public class AutoID
    {
        private static int objIDX = 1;
        private static string objDayTime = "";
        private static object objLock = new object();
        public AutoID()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetGuid()
        {
            return System.Guid.NewGuid().ToString().ToLower();
        }

        public static string getAutoID()
        {
            return getAutoID("000000");
        }
       

        public static string getAutoID(String cORG_ID)
        {
            lock (objLock)
            {
                String cNowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                Random iCode = new Random();
                if (cNowTime.Equals(objDayTime))
                {
                    objIDX = objIDX + 1;
                }
                else
                {
                    objDayTime = cNowTime;
                    objIDX = 1;
                }
                return objDayTime + objIDX.ToString("D6") + iCode.Next(10000).ToString("D4") + cORG_ID;
            }
        }
    }
}