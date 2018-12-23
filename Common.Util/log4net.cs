﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
namespace TLKJ.Utils
{
    /// <summary>
    /// Summary description for RoleClass
    /// </summary>
    public class log4net
    {
        public log4net()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void WriteLogFile(String cStr)
        {
            WriteLogFile(cStr, 1);
        }
        public static void WriteLogFile(String cStr, int iLogLevel)
        {
            StreamWriter sw = null;
            try
            {
                String cAppDir = AppDomain.CurrentDomain.BaseDirectory;
                String cLogDir = cAppDir + "logs";
                if (!Directory.Exists(cLogDir))
                {
                    Directory.CreateDirectory(cLogDir);
                }
                String cLogName = "log_" + iLogLevel.ToString() + ".txt";
                string curDT = System.DateTime.Now.ToString("yyyyMMdd");
                String logFile = cLogDir + "\\" + curDT + cLogName;


                if (File.Exists(logFile))
                {
                    sw = File.AppendText(logFile);
                }
                else
                {
                    sw = File.CreateText(logFile);
                }
                sw.WriteLine("--------" + System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString() + "--------");
                HttpContext ctx = HttpContext.Current;
                if (ctx != null)
                {
                    String cUserIP = ctx.Request.UserHostAddress;
                    HttpRequest request = ctx.Request;
                    String cUrl = request.RawUrl;
                    DateTime dtTime = DateTime.Now;
                    sw.WriteLine("访问地址：" + cUrl);
                    sw.WriteLine("用户地址：" + cUserIP);

                    string getAddressAndMessage = request.ServerVariables["Http_Referer"];
                    if (request != null)
                    {
                        if (getAddressAndMessage != null)
                        {
                            sw.WriteLine("登陆页面：" + getAddressAndMessage);
                        }

                    }
                }
                if (cStr != "")
                {
                    sw.WriteLine("操作内容：" + cStr);
                }
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sw != null)
                {
                    try
                    {
                        sw.Flush();
                    }
                    catch { }
                    try
                    {
                        sw.Close();
                    }
                    catch { }
                    try
                    {
                        sw.Dispose();
                    }
                    catch { }
                    sw = null;
                }
            }
        }
    }

}