
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
    public class CutTask
    {
        public static void Execute()
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));

            String cUPLOAD_PATH = INIConfig.ReadString("ANALYSE", "DFS_PATH", "");
            String cDFS_TYPE = INIConfig.ReadString("ANALYSE", "DFS_TYPE", "");

            String cAppDir = Application.StartupPath;
            Boolean isUpload = false;
            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC";
            while (!ApplicationEvent.isImgCutAbort)
            {
                log4net.WriteLogFile("分析线程正在运行中......");
                String cFileName = getFileName();
                if (String.IsNullOrWhiteSpace(cFileName))
                {
                    break;
                }

                String cFileExt = Path.GetExtension(cFileName);
                String cREC_ID = Path.GetFileName(cFileName).Replace(cFileExt, "");
                List<KeyValue> ImageList = IMGAI.getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
                String cExportFileName = Application.StartupPath + "\\" + cREC_ID + ".zip";
                if (ImageList == null)
                {
                    continue;
                }
                List<String> sqls = new List<string>();
                if (ImageList.Count > 0)
                {
                    if (File.Exists(cExportFileName))
                    {
                        isUpload = CopyUnit.SSH_Upload(cExportFileName, "ANALYSE");
                    }
                }
                int iCode = 0;
                if (isUpload)
                {

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
                    iCode = DbManager.ExecSQL(sqls);

                }


                if (iCode > 0)
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
                aMaster.AddField("AI_FLAG", 1);
                iCode = DbManager.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
                if (iCode > 0)
                {
                    log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片抠图成功！");
                }

                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile(ex.Message);
                }
            }
        }
        public static Queue<String> ImageQueueList = null;
        public static String getFileName()
        {
            if (ImageQueueList == null)
            {
                ImageQueueList = new Queue<string>();
            }

            if (ImageQueueList.Count == 0)
            {
                String cDFS_PATH = INIConfig.ReadString("ANALYSE", "FILE_PATH");
                if (!Directory.Exists(cDFS_PATH))
                {
                    return null;
                }
                String[] FileList = Directory.GetFiles(cDFS_PATH);
                for (int i = 0; i < FileList.Length; i++)
                {
                    String cFileName = FileList[i];
                    ImageQueueList.Enqueue(cFileName);
                    if (i > 10)
                    {
                        break;
                    }
                }
            }
            if (ImageQueueList.Count > 0)
            {
                String cFileName = ImageQueueList.Dequeue();
                return cFileName;
            }
            else
            {
                return null;
            }
        }
    }
}
