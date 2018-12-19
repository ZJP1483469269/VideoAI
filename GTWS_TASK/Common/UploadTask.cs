
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.DB;
using TLKJ.Utils;
using Renci.SshNet;
using TLKJAI;
using System.Threading;

namespace TLKJ_IVS
{
    public class UploadTask
    {
    
        public static void Execute()
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));

            int iALARM_ALLOW = INIConfig.ReadInt("ALARM", "DFS_ALLOW");
            String cALARM_PATH = INIConfig.ReadString("ANALYSE", "DFS_PATH", "0");

            String cAppDir = Application.StartupPath;

            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC";
            List<String> FileList = new List<string>();
            while (!ApplicationEvent.isUploadAbort)
            {
                Boolean isUpload = false;
                DataTable dtRows = DbManager.QueryData(" select TOP 1 *  from XT_IMG_REC where UPLOAD_FLAG=0  ");
                for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
                {
                    String cREC_ID = StringEx.getString(dtRows, i, "REC_ID");
                    String cFILE_URL = StringEx.getString(dtRows, i, "FILE_URL");
                    FileList.Clear();
                    String cFileName = cAppDir + cFILE_URL;
                    Boolean UploadFlag = false;
                    if (!File.Exists(cFileName))
                    {
                        aMaster.ClearField();
                        aMaster.AddField("UPLOAD_FLAG", 1);
                        aMaster.AddField("ALARM_FLAG", 2);
                        log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片不存在跳过！");
                        int iCode = DbManager.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
                        if (iCode > 0)
                        {
                            continue;
                        }
                    }

                    UploadFlag = UploadTask.Upload(cFileName, "ALARM");
                    if (UploadFlag)
                    {
                        aMaster.ClearField();
                        aMaster.AddField("UPLOAD_FLAG", 1);
                        aMaster.AddField("ALARM_FLAG", 0);
                        int iCode = DbManager.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
                        log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片，已处理！");
                        try
                        {
                            File.Delete(cFileName);
                        }
                        catch (Exception ex)
                        {
                            log4net.WriteLogFile(ex.Message);
                        }
                    }
                }
                try
                {
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                }
            }
        }

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
                return CopyUnit.SSH_Upload(cFileName, cType);
            }
            else if (cDFSType.Equals("POST"))
            {
                String cDFSUrl = "http://" + cDFS_PATH + "/api/dfs.ashx";
                return CopyUnit.PostFile(cFileName, cDFSUrl);
            }
            else if (cDFSType.Equals("COPY"))
            {
                return CopyUnit.CopyFile(cFileName, cDFS_PATH);
            }
            else
            {
                log4net.WriteLogFile("参数配置错误！");
                return false;
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
    }
}
