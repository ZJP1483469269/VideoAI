
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
using System.Threading;

namespace TLKJ_IVS
{
    public class UploadTask
    {
        private static int iMinVal;
        private static int iMaxVal;
        private static int iGrayMinVal;
        private static int iGrayMaxVal;
        private static int iALARM_ALLOW;
        private static String cALARM_PATH;

        private static int iANALYSE_ALLOW;
        private static String cANALYSE_PATH;
        private static String cAppDir = Application.StartupPath;
        public static void Init()
        {
            iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));

            iALARM_ALLOW = INIConfig.ReadInt("ALARM", "DFS_ALLOW");
            cALARM_PATH = INIConfig.ReadString("ALARM", "DFS_PATH", "");

            iANALYSE_ALLOW = INIConfig.ReadInt("ANALYSE", "DFS_ALLOW");
            cANALYSE_PATH = INIConfig.ReadString("ANALYSE", "DFS_PATH", "");
        }
        public static Queue<String> AlarmFileList = null;
        public static Queue<String> AnalyseFileList = null;
        public static String getAlarmFile()
        {
            if (cAppDir == null)
            {
                cAppDir = Application.StartupPath;
            }
            Init();
            if (AlarmFileList == null)
            {
                AlarmFileList = new Queue<string>();
            }

            if (AlarmFileList.Count == 0)
            {
                String cFILE_PATH = INIConfig.ReadString("ALARM", "FILE_PATH", @"\images\");
                String cDFS_PATH = cAppDir + cFILE_PATH;
                if (!Directory.Exists(cDFS_PATH))
                {
                    return null;
                }
                String[] FileList = Directory.GetFiles(cDFS_PATH);
                for (int i = 0; i < FileList.Length; i++)
                {
                    String cFileName = FileList[i];
                    AlarmFileList.Enqueue(cFileName);
                    if (i > 10)
                    {
                        break;
                    }
                }
            }
            if (AlarmFileList.Count > 0)
            {
                String cFileName = AlarmFileList.Dequeue();
                return cFileName;
            }
            else
            {
                return null;
            }
        }

        public static String getAnalyseFile()
        {
            if (cAppDir == null)
            {
                cAppDir = Application.StartupPath;
            }
            Init();
            if (AnalyseFileList == null)
            {
                AnalyseFileList = new Queue<string>();
            }

            if (AnalyseFileList.Count == 0)
            {
                String cFILE_PATH = INIConfig.ReadString("ANALYSE", "FILE_PATH", "");
                if (!Directory.Exists(cFILE_PATH))
                {
                    return null;
                }
                String[] FileList = Directory.GetFiles(cFILE_PATH);
                for (int i = 0; i < FileList.Length; i++)
                {
                    String cFileName = FileList[i];
                    AnalyseFileList.Enqueue(cFileName);
                    if (i > 10)
                    {
                        break;
                    }
                }
            }
            if (AnalyseFileList.Count > 0)
            {
                String cFileName = AnalyseFileList.Dequeue();
                return cFileName;
            }
            else
            {
                return null;
            }
        }
        private static bool UpdateAlarmFile(String cFileName)
        {
            JActiveTable aMaster = new JActiveTable();
            aMaster.TableName = "XT_IMG_REC";

            String cFileExt = Path.GetExtension(cFileName);
            String cREC_ID = Path.GetFileName(cFileName).Replace(cFileExt, "");

            Boolean UploadFlag = UploadTask.Upload(cFileName, "ALARM", "UPLOAD_FLAG");
            if (UploadFlag)
            {
                log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片，已上传！");
                aMaster.ClearField();
                aMaster.AddField("UPLOAD_FLAG", 1);
                aMaster.AddField("UPDATE_TIME", DateUtils.getDayTimeNum());
                int iCode = WebSQL.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));

                try
                {
                    File.Delete(cFileName);
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile("UploadTask.Execute." + ex.Message);
                }
                log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片，本删除成功！");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateAnalyseFile(String cFileName)
        {
            Boolean isUpload = false;
            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC";

            log4net.WriteLogFile("分析线程正在运行中......");
            if (String.IsNullOrWhiteSpace(cFileName))
            {
                return false;
            }

            String cFileExt = Path.GetExtension(cFileName);
            String cREC_ID = Path.GetFileName(cFileName).Replace(cFileExt, "");
            List<KeyValue> ImageList = IMGAI.getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
            String cExportFileName = Application.StartupPath + "\\" + cREC_ID + ".zip";
            int iCode = 0;
            Boolean MustRemoveFile = false;
            if ((ImageList != null) && (ImageList.Count > 0))
            {
                List<String> sqls = new List<string>();
                if (File.Exists(cExportFileName))
                {
                    SftpClient ftp = getAnalyseClient();
                    isUpload = CopyUnit.SSH_Upload(ftp, cExportFileName, "ANALYSE");
                }

                if (isUpload)
                {
                    MustRemoveFile = true;
                    log4net.WriteLogFile("UploadTask.文件上传成功！");
                    for (int k = 0; (ImageList != null) && (k < ImageList.Count); k++)
                    {
                        Application.DoEvents();
                        KeyValue rowKey = ImageList[k];
                        aSlave.ClearField();
                        String cKeyID = StringEx.getString(k + 1000);
                        aSlave.AddField("ID", AutoID.getAutoID() + "_" + cKeyID);
                        aSlave.AddField("ALARM_FLAG", 0);
                        aSlave.AddField("REC_ID", cREC_ID);
                        aSlave.AddField("CREATE_TIME", DateUtils.getDayTimeNum());
                        aSlave.AddField("POINT_LIST", rowKey.Val);
                        sqls.Add(aSlave.getInsertSQL());
                    }
                    iCode = WebSQL.ExecSQL(sqls);
                }
                else
                {
                    log4net.WriteLogFile("UploadTask." + cREC_ID + ":文件上传失败！");
                }
            }
            else
            {
                log4net.WriteLogFile("UploadTask." + cREC_ID + ":图片拆分失败！");
                MustRemoveFile = true;
            }
            if (MustRemoveFile)
            {
                try
                {
                    File.Delete(cFileName);
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile(ex.Message);
                }
                try
                {
                    File.Delete(cExportFileName);
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile(ex.Message);
                }
            }

            aMaster.ClearField();
            if (iCode > 0)
            {
                aMaster.AddField("AI_FLAG", 1);
            }
            else
            {
                aMaster.AddField("AI_FLAG", 2);
            }
            iCode = WebSQL.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
            if (iCode > 0)
            {
                log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片抠图成功！");
            }
            return true;
        }

        public static void Execute()
        {
            while (!ApplicationEvent.isUploadAbort)
            {
                Boolean Find_File_Flag = false;
                log4net.WriteLogFile("UploadTask.Execute..", LogType.DEBUG);
                String cFileName = getAlarmFile();
                if (String.IsNullOrWhiteSpace(cFileName))
                {
                    log4net.WriteLogFile("UploadTask.Execute..未发现图片", LogType.DEBUG);
                }
                else
                {
                    Find_File_Flag = true;
                    UpdateAlarmFile(cFileName);
                }
                try
                {
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile("UploadTask.Sleep." + ex.Message);
                }

                cFileName = getAnalyseFile();
                if (String.IsNullOrWhiteSpace(cFileName))
                {
                    log4net.WriteLogFile("UploadTask.Execute..未发现图片", LogType.DEBUG);
                }
                else
                {
                    UpdateAnalyseFile(cFileName);
                    if (!Find_File_Flag)
                    {
                        Find_File_Flag = true;
                    }
                }
                if (Find_File_Flag)
                {
                    try
                    {
                        Thread.Sleep(500);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile("UploadTask.Sleep." + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        Thread.Sleep(1000 * 3);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile("UploadTask.Sleep." + ex.Message);
                    }
                }
            }
        }

        private static SftpClient AlarmClient = null;
        public static SftpClient getAlarmClient()
        {
            if (AlarmClient == null)
            {
                String cDFSType = "ALARM";
                String cDFS_HOST = INIConfig.ReadString(cDFSType, "DFS_HOST", "");
                String cDFS_PORT = INIConfig.ReadString(cDFSType, "DFS_PORT", "22");
                int iDFS_PORT = StringEx.getInt(cDFS_PORT);
                String cDFS_USER = INIConfig.ReadString(cDFSType, "DFS_USER", "root");
                String cDFS_PASS = INIConfig.ReadString(cDFSType, "DFS_PASS", "");
                AlarmClient = new SftpClient(cDFS_HOST, iDFS_PORT, cDFS_USER, cDFS_PASS);
            }
            return AlarmClient;
        }


        private static SftpClient AnalyseClient = null;
        public static SftpClient getAnalyseClient()
        {
            if (AnalyseClient == null)
            {
                String cDFSType = "ANALYSE";
                String cDFS_HOST = INIConfig.ReadString(cDFSType, "DFS_HOST", "");
                String cDFS_PORT = INIConfig.ReadString(cDFSType, "DFS_PORT", "22");
                int iDFS_PORT = StringEx.getInt(cDFS_PORT);
                String cDFS_USER = INIConfig.ReadString(cDFSType, "DFS_USER", "root");
                String cDFS_PASS = INIConfig.ReadString(cDFSType, "DFS_PASS", "");
                AnalyseClient = new SftpClient(cDFS_HOST, iDFS_PORT, cDFS_USER, cDFS_PASS);
            }
            return AnalyseClient;
        }
        public static Boolean Upload(String cFileName, String cType, String cFieldName)
        {
            if (!File.Exists(cFileName))
            {
                log4net.WriteLogFile("UploadTask..文件不存在..." + cFileName);
                return false;
            }
            String cDFS_PATH = INIConfig.ReadString(cType, "DFS_PATH", "");
            String cDFSType = INIConfig.ReadString(cType, "DFS_TYPE", "");

            if (cDFSType.Equals("SSH"))
            {
                SftpClient ftp = getAlarmClient();
                return CopyUnit.SSH_Upload(AlarmClient, cFileName, cType);
            }
            else if (cDFSType.Equals("POST"))
            {
                String cDFSUrl = "http://" + cDFS_PATH + "/api/dfs.ashx?UPLOAD_FIELD=" + cFieldName;
                return CopyUnit.PostFile(cFileName, cDFSUrl);
            }
            else if (cDFSType.Equals("COPY"))
            {
                return CopyUnit.CopyFile(cFileName, cDFS_PATH);
            }
            else
            {
                log4net.WriteLogFile("UploadTask..参数配置错误！");
                return false;
            }
        }
    }
}
