
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

            DataTable dtRows = DbManager.GetDataTable(" select TOP 5 *  from XT_IMG_REC where UPLOAD_FLAG=0  ");
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
                        UploadFlag = CopyFile(cFileName, cUPLOAD_PATH);
                    }
                    if (UploadFlag)
                    {
                        aMaster.ClearField();
                        aMaster.AddField("UPLOAD_FLAG", 1);
                        aMaster.AddField("ALARM_FLAG", 0);
                        log4net.WriteTextLog("REC_ID为：" + cREC_ID + "的图片上传成功！");
                        int iCode = DbManager.ExeSql(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' ")).Code;
                        if (iCode > 0)
                        {
                            log4net.WriteTextLog("REC_ID为：" + cREC_ID + "的图片抠图成功！");
                        }
                        isUpload = true;
                    }
                    Application.DoEvents();
                    isUpload = false;
                    if (iEXPORT_IMAGE > 0)
                    {
                        List<String> ImageList = TLKJ_AI.getImageList(cFileName, iMinVal, iMaxVal, iGrayMinVal, iGrayMaxVal);
                        List<String> sqls = new List<string>();
                        for (int k = 0; (ImageList != null) && (k < ImageList.Count); k++)
                        {
                            Application.DoEvents();
                            String cImageFileName = ImageList[k];
                            if (iDFS_FLAG == 1)
                            {
                                UploadFlag = Upload(cImageFileName, cDFS_PATH);
                            }
                            else
                            {
                                UploadFlag = CopyFile(cImageFileName, cUPLOAD_PATH);
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
                                sqls.Add(aSlave.getInsertSQL());
                            }
                        }
                        int iCode = DbManager.ExeSql(sqls).Code;
                        if (iCode > 0)
                        {
                            isUpload = true;
                        }
                    }

                    if (isUpload)
                    {
                        aMaster.ClearField();
                        aSlave.AddField("AI_FLAG", 1);
                        int iCode = DbManager.ExeSql(aMaster.getUpdateSQL(" REC_ID='" + cREC_ID + "' ")).Code;
                        if (iCode > 0)
                        {
                            log4net.WriteTextLog("REC_ID为：" + cREC_ID + "的图片抠图成功！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log4net.WriteTextLog("图片上传异常: " + ex);
                }
            }
        }


        private static SftpClient sftp = null;
        public static Boolean Upload(String cFilePath, String cDFSPath)
        {
            if (!File.Exists(cFilePath))
            {
                return false;
            }
            Boolean isConnected = false;
            if (sftp != null)
            {
                try
                {

                    sftp.Connect();
                    isConnected = true;
                }
                catch (Exception ex)
                {
                    sftp = null;
                }
            }
            if (!isConnected)
            {
                String cDFS_HOST = INIConfig.ReadString("UPLOAD", "DFS_HOST", "0");
                String cDFS_PORT = INIConfig.ReadString("UPLOAD", "DFS_PORT", "0");
                String cDFS_USER = INIConfig.ReadString("UPLOAD", "DFS_USER", "0");
                String cDFS_PASS = INIConfig.ReadString("UPLOAD", "DFS_PASS", "0");
                sftp = new SftpClient(cDFS_HOST, cDFS_USER, cDFS_PASS);
            }
            FileStream fs = null;
            try
            {
                sftp.Connect();
                fs = new FileStream(cFilePath, FileMode.Open);
                String cFileName = Path.GetFileName(cFilePath);
                sftp.CreateDirectory(cDFSPath);
                sftp.UploadFile(fs, cDFSPath + cFileName);
                return true;
            }
            catch (Exception ex)
            {
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
                        log4net.WriteTextLog(ex.Message);
                    }
                }
            }
        }

        private static bool CopyFile(string cFileName, string cDFS_PATH)
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
    }
}
