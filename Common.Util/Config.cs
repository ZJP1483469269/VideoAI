using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;

namespace TLKJ.Utils
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class Config
    {
        public static object objLock = new object();
        public Config()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }
        public static string GetAppSettings(string cKey)
        {
            return GetAppSettings(cKey, "");
        }
        /// <summary>
        /// 获取webconfig配置文件
        /// </summary>
        /// <param name="cKey"></param>
        /// <returns></returns>
        public static string GetAppSettings(string cKey, string cDefVal)
        {
            string result = "";
            cKey = (cKey == null) ? "" : cKey.Trim();
            try
            {
               
               
                result = ConfigurationManager.AppSettings[cKey].ToString();
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message); 
                result = cDefVal;
            }
            result = (result == null) ? "" : result.Trim();
            return result;

        }
    }
}