using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
namespace TLKJ.Utils
{
    /// <summary>
    /// Summary description for RoleClass
    /// </summary>
    public class FileLib
    {
        public FileLib()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static String ReadTextFile(String cFileName)
        {
            if (!File.Exists(cFileName))
            {
                return null;
            }
            try
            {
                string cStr = File.ReadAllText(cFileName);
                return cStr;
            }
            catch (Exception ex)
            {
                 log4net.WriteTextLog(ex.Message);
                return null;
            }
        }
        public static Boolean WriteTextFile(String cFileName, String cContent)
        {
            StreamWriter sw = null;
            if (File.Exists(cFileName))
            {
                File.Delete(cFileName);
            }
            try
            {

                sw = File.CreateText(cFileName);

                sw.WriteLine(cContent);

                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
                return true;
            }
            catch (Exception ex)
            {
                 log4net.WriteTextLog(ex.Message);
                return false;
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