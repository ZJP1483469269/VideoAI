using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Net;
/// <summary>
/// AppManager 的摘要说明
/// </summary>
/// 
namespace TLKJ.Utils
{
    public class SMSUtil
    {
        public static object objLock = new object();
        public static Boolean SendSMS(String cTelNO, String cMSG)
        {
            SMSUtil vo = new SMSUtil();
            String cUserID = "gtsms";
            String cUserPass = "619af7456ec341d6af33";
            String cUrl = "http://utf8.api.smschinese.cn/?Uid=" + cUserID + "&Key=" + cUserPass + "&smsMob=" + cTelNO + "&smsText=" + cMSG;
            return vo.PostToWeb(cUrl);
        }

        public Boolean PostToWeb(string url)
        {
            Boolean isSendFlag = false;
            if (url == null || url.Trim().ToString() == "")
            {
                return isSendFlag;
            }
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.UTF8);
                String cStr = ser.ReadToEnd();
                if (cStr.Equals("1"))
                {
                    isSendFlag = true;
                }
                else
                {
                    isSendFlag = false;
                }
            }
            catch (Exception ex)
            {
                isSendFlag = false;
            }
            return isSendFlag;
        }
    }
}