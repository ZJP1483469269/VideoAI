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

        public static List<String> getFileLines(String cFileName)
        {
            List<string> FileList = new List<string>();
            try
            {
                string cStr;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(cFileName))
                {
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((cStr = sr.ReadLine()) != null)
                    {
                        FileList.Add(cStr);
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return FileList;
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