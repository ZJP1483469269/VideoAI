
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using TLKJ.Utils;
using Renci.SshNet;
using TLKJAI;

namespace TLKJ_IVS
{
    public class CopyUnit
    {
        private static SftpClient sftp = null;
        public static Boolean Upload(String cFileName, String cType)
        {
            if (!File.Exists(cFileName))
            {
                return false;
            }
            String cDFS_PATH = INIConfig.ReadString(cType, "DFS_PATH", "");
            String cDFSType = INIConfig.ReadString(cType, "DFS_TYPE", "");

            if (cDFSType.Equals("SSH"))
            {
                return SSH_Upload(cFileName, cType);
            }
            else if (cDFSType.Equals("POST"))
            {
                String cDFSUrl = "http://" + cDFS_PATH + "/api/dfs.ashx";
                return PostFile(cFileName, cDFSUrl);
            }
            else if (cDFSType.Equals("COPY"))
            {
                return CopyFile(cFileName, cDFS_PATH);
            }
            else
            {
                log4net.WriteLogFile("参数配置错误！");
                return false;
            }
        }

        public static Boolean SSH_Upload(String cFileName, String cDFSType)
        {
            if (sftp == null)
            {
                String cDFS_HOST = INIConfig.ReadString(cDFSType, "DFS_HOST", "");
                String cDFS_PORT = INIConfig.ReadString(cDFSType, "DFS_PORT", "22");
                int iDFS_PORT = StringEx.getInt(cDFS_PORT);
                String cDFS_USER = INIConfig.ReadString(cDFSType, "DFS_USER", "root");
                String cDFS_PASS = INIConfig.ReadString(cDFSType, "DFS_PASS", "");
                sftp = new SftpClient(cDFS_HOST, iDFS_PORT, cDFS_USER, cDFS_PASS);
                try
                {
                    sftp.Connect();
                }
                catch (Exception ex)
                {
                    sftp = null;
                }
            }

            if (sftp == null)
            {
                return false;
            }

            FileStream fs = null;
            try
            {
                String cDFS_PATH = INIConfig.ReadString(cDFSType, "DFS_PATH", "");
                fs = new FileStream(cFileName, FileMode.Open);
                String cStr = Path.GetFileName(cFileName);
                try
                {
                    sftp.CreateDirectory(cDFS_PATH);
                }
                catch (Exception ex)
                {

                }
                String cDFSPath = cDFS_PATH;
                if (!cDFSPath.EndsWith("/"))
                {
                    cDFSPath = cDFSPath + "/";
                }
                sftp.UploadFile(fs, cDFSPath + cStr);
                return true;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                sftp = null;
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    try
                    {
                        fs.Close();
                        fs = null;
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message);
                    }
                }
            }
        }

        public static Boolean RemoveFileList(List<KeyValue> ImageList)
        {
            String cFileDir = "";
            for (int k = ImageList.Count - 1; (k >= 0); k--)
            {
                KeyValue rowKey = ImageList[k];
                String cImageFileName = rowKey.Text;
                if (String.IsNullOrEmpty(cFileDir))
                {
                    cFileDir = Path.GetDirectoryName(cImageFileName);
                }

                if (File.Exists(cImageFileName))
                {
                    try
                    {
                        File.Delete(cImageFileName);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message);
                    }
                }
            }
            return true;
        }
        public static Boolean RemoveFileDir(String cFileDir)
        {
            try
            {
                Directory.Delete(cFileDir);
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
            }
            return true;
        }
        public static Boolean RemoveFile(String cFileDir)
        {
            try
            {
                File.Delete(cFileDir);
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
            }
            return true;
        }
         
        public static bool CopyFile(string cFileName, string cDFS_PATH)
        {
            try
            {
                File.Copy(cFileName, cDFS_PATH + Path.GetFileName(cFileName));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool PostFile(string cFileName, string cUrl)
        {
            try
            {
                HttpUtil vPost = new HttpUtil();
                String cUpload = vPost.HttpPostFile(cUrl, cFileName);
                return cUpload.Equals("1");
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
