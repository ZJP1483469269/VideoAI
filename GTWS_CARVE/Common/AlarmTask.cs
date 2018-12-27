
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
    public class AlarmTask
    {
        private int iMinVal;
        private int iMaxVal;
        private int iGrayMinVal;
        private int iGrayMaxVal;
        private int iALARM_ALLOW;
        private String cALARM_PATH;

        private int iANALYSE_ALLOW;
        private String cANALYSE_PATH;
        private String cAppDir;

        private static AlarmTask instance = null;

        private AlarmTask()
        {
            iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));

            iALARM_ALLOW = INIConfig.ReadInt("ALARM", "DFS_ALLOW");
            cALARM_PATH = INIConfig.ReadString("ALARM", "DFS_PATH", "");

            iANALYSE_ALLOW = INIConfig.ReadInt("ANALYSE", "DFS_ALLOW");
            cANALYSE_PATH = INIConfig.ReadString("ANALYSE", "DFS_PATH", "");

            cAppDir = Application.StartupPath;
        }

        public static AlarmTask getInstance()
        {
            if (instance == null)
            {
                instance = new AlarmTask();
            }
            return instance;
        }

        public static Queue<String> AlarmFileList = null;

        public String getAlarmFile()
        {
            if (cAppDir == null)
            {
                cAppDir = Application.StartupPath;
            }
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


        private bool UpdateAlarmFile(String cFileName)
        {
            JActiveTable aMaster = new JActiveTable();
            aMaster.TableName = "XT_IMG_REC";

            String cFileExt = Path.GetExtension(cFileName);
            String cREC_ID = Path.GetFileName(cFileName).Replace(cFileExt, "");
            String cWebUrl = INIConfig.ReadString("Config", AppConfig.WEB_URL);
            String cUrl = "http://" + cWebUrl + "/api/dfs.ashx?UPLOAD_FIELD=UPLOAD_FLAG";
            Boolean UploadFlag = CopyUnit.PostFile(cFileName, cWebUrl);
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


        public static void Execute()
        {
            AnalyseTask Task = AnalyseTask.getInstance(); 
            int iANALYSE_ALLOW = INIConfig.ReadInt("ANALYSE", "DFS_ALLOW");
            while (!ApplicationEvent.isUploadAbort)
            {
                Boolean Find_File_Flag = false;
                log4net.WriteLogFile("UploadTask.Execute..", LogType.DEBUG);
                String cFileName = null;
                
                if (iANALYSE_ALLOW > 0)
                {
                    cFileName = Task.getAnalyseFile();
                    if (String.IsNullOrWhiteSpace(cFileName))
                    {
                        log4net.WriteLogFile("UploadTask.Execute..未发现图片", LogType.DEBUG);
                    }
                    else
                    {
                        Task.UpdateAnalyseFile(cFileName);
                        if (!Find_File_Flag)
                        {
                            Find_File_Flag = true;
                        }
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
        public SftpClient getAlarmClient()
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

            if (!AlarmClient.IsConnected)
            {
                try
                {
                    AlarmClient.Connect();
                    return AlarmClient;
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile(ex.Message);
                    AlarmClient = null;
                    return null;
                }
            }
            else
            {
                return AlarmClient;
            }
        }
    }
}
