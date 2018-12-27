
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
        public static Boolean SSH_Upload(SftpClient sftp, String cFileName, String cDFSType)
        {
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
                log4net.WriteLogFile("CopyUnit..UploadFile." + ex.Message, LogType.ERROR);
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
                        log4net.WriteLogFile("CopyUnit.." + ex.Message, LogType.ERROR);
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
                        log4net.WriteLogFile("CopyUnit.." + ex.Message);
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
                log4net.WriteLogFile("CopyUnit.." + ex.Message);
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
