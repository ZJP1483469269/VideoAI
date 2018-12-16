
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

            String cDFS_PATH = INIConfig.ReadString("UPLOAD", "DFS_PATH", "0");
            String cUPLOAD_PATH = INIConfig.ReadString("UPLOAD", "UPLOAD_PATH", "");

            int iDFS_FLAG = StringEx.getInt(INIConfig.ReadString("Config", "DFS_FLAG", "0"));

            DataTable dtRows = DbManager.QueryData(" select TOP 5 *  from XT_IMG_REC where UPLOAD_FLAG=0  ");
            String cAppDir = Application.StartupPath;
            Boolean isUpload = false;
            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC";

            for (int i = 0; (dtRows != null) && (i < dtRows.Rows.Count); i++)
            {
                Application.DoEvents();
                try
                {
                    String cREC_ID = StringEx.getString(dtRows, i, "REC_ID");
                    String cFILE_URL = StringEx.getString(dtRows, i, "FILE_URL");

                    String cFileName = cAppDir + cFILE_URL;
                    Boolean UploadFlag = false;
                    if (iDFS_FLAG == 1)
                    {
                        UploadFlag = Upload(cFileName, cDFS_PATH);
                    }
                    else
                    {
                        UploadFlag = CopyUtil.CopyFile(cFileName, cUPLOAD_PATH);
                    }
                    if (UploadFlag)
                    {
                        aMaster.ClearField();
                        aMaster.AddField("UPLOAD_FLAG", 1);
                        aMaster.AddField("ALARM_FLAG", 0);
                        log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片上传成功！");
                        int iCode = DbManager.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
                        if (iCode > 0)
                        {
                            log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片抠图成功！");
                        }
                        isUpload = true;
                    }
                    Application.DoEvents();
                    isUpload = false;
                    if (iEXPORT_IMAGE > 0)
                    {
                        List<KeyValue> ImageList = IMGAI.getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
                        List<String> sqls = new List<string>();
                        for (int k = 0; (ImageList != null) && (k < ImageList.Count); k++)
                        {
                            Application.DoEvents();
                            KeyValue rowKey = ImageList[k];
                            String cImageFileName = rowKey.Text;
                            if (iDFS_FLAG == 1)
                            {
                                UploadFlag = Upload(cImageFileName, cDFS_PATH);
                            }
                            else
                            {
                                UploadFlag = CopyUtil.CopyFile(cImageFileName, cUPLOAD_PATH);
                            }

                            if (UploadFlag)
                            {
                                isUpload = true;
                                aSlave.ClearField();
                                aSlave.AddField("AI_FLAG", 1);
                                String cKeyID = StringEx.getString(k + 1000);
                                aSlave.AddField("ID", AutoID.getAutoID() + "_" + cKeyID);
                                aSlave.AddField("REC_ID", cREC_ID);
                                aSlave.AddField("CAMERA_ID", cREC_ID);
                                aSlave.AddField("FILE_URL", cDFS_PATH + cImageFileName);
                                aSlave.AddField("CREATE_TIME", DateUtils.getDayTimeNum());
                                aSlave.AddField("POINT_LIST", rowKey.Val);
                                sqls.Add(aSlave.getInsertSQL());
                            }
                        }
                        int iCode = DbManager.ExecSQL(sqls);
                        if (iCode > 0)
                        {
                            isUpload = true;
                        }
                    }

                    if (isUpload)
                    {
                        aMaster.ClearField();
                        aSlave.AddField("AI_FLAG", 1);
                        int iCode = DbManager.ExecSQL(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' "));
                        if (iCode > 0)
                        {
                            log4net.WriteLogFile("REC_ID为：" + cREC_ID + "的图片抠图成功！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile("图片上传异常: " + ex);
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
                return CopyUtil.SSH_Upload(cFileName, cType);
            }
            else if (cDFSType.Equals("POST"))
            { 
                String cDFSUrl = "http://" + cDFS_PATH + "/api/dfs.ashx";
                return CopyUtil.PostFile(cFileName, cDFSUrl);
            }
            else if (cDFSType.Equals("COPY"))
            {
                return CopyUtil.CopyFile(cFileName, cDFS_PATH);
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
