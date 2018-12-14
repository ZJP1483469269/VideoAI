using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;

namespace TLKJ.DAO
{
    public class PackUtil
    {
        public static String Pack(String cTABLE_ID, DataTable dtRows, String cRootDir, String cSYSID)
        {
            List<ExportObject> arKeys = new List<ExportObject>();
            DataTable dtFieldConfig = DbManager.QueryData("SELECT FIELD_NAME,ISKEY FROM S_ETL_FIELD WHERE TABLE_ID='" + cTABLE_ID + "'");
            for (int j = 0; (dtRows != null) && (j < dtRows.Rows.Count); j++)
            {
                ExportObject vo = new ExportObject();
                vo.TABLE_ID = cTABLE_ID;
                vo.SYS_ID = cSYSID;
                String cKEY_ID = StringEx.getString(dtRows, 0, "ID").ToUpper();
                for (int k = 0; (dtFieldConfig != null) && (k < dtFieldConfig.Rows.Count); k++)
                {
                    String cFieldName = StringEx.getString(dtFieldConfig, k, "FIELD_NAME").ToUpper();
                    String cFieldValue = StringEx.getString(dtRows, j, cFieldName);
                    vo.AddFieldValue(cFieldName, Base64.StrToBase64(cFieldValue));
                }
                String cFileID_List = StringEx.getString(dtRows, 0, "FILES_ID").ToUpper();
                if (cFileID_List.Length > 0)
                {
                    String[] File_List = cFileID_List.Split(',');
                    cFileID_List = "";
                    for (int i = 0; i < File_List.Length; i++)
                    {
                        if (cFileID_List == "")
                        {
                            cFileID_List = "'" + File_List[i] + "'";
                        }
                        else
                        {
                            cFileID_List = cFileID_List + "," + "'" + File_List[i] + "'";
                        }
                    }

                    DataTable dtFiles = DbManager.QueryData("SELECT ID,TEXT,URL FROM S_UPLOAD WHERE ID in (" + cFileID_List + ")");
                    for (int k = 0; (dtFiles != null) && (k < dtFiles.Rows.Count); k++)
                    {
                        String cID = StringEx.getString(dtFiles, k, "ID").ToUpper();
                        String cText = StringEx.getString(dtFiles, k, "TEXT").ToUpper();
                        String cUrl = StringEx.getString(dtFiles, k, "URL").ToUpper();
                        String cFileName = cRootDir + cUrl.Replace("/", "\\");
                        if (File.Exists(cFileName))
                        {
                            vo.AddFileValue(cID, cText, cUrl, Base64.StrToBase64(cFileName));
                        }
                    }
                }
                arKeys.Add(vo);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(arKeys);
        }

        public static List<ExportObject> Explain(String cStr)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExportObject>>(cStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int UpdateDB(List<ExportObject> Rows, String cRootDir)
        {
            List<String> sqls = new List<string>();
            StringBuilder sql = new StringBuilder();
            for (int i = 0; i < Rows.Count; i++)
            {
                ExportObject vo = Rows[i];
                String cTABLE_ID = vo.TABLE_ID;
                String cTableName = DbManager.GetStrValue("SELECT TABLE_NAME FROM S_ETL_TABLE WHERE TABLE_ID='" + cTABLE_ID + "'");
                DataTable dtFieldList = DbManager.QueryData("SELECT FIELD_NAME,ISKEY FROM S_ETL_FIELD WHERE TABLE_ID='" + cTABLE_ID + "'");
                String cSYS_ID = vo.SYS_ID;
                List<ExportField> FieldList = vo.rows.FieldList;
                List<ExportFile> FileList = vo.rows.FileList;
                sql.Clear();

                List<String> KeyField = new List<string>();
                String cWhereParm = null;
                String cFieldList = null;
                String cValueList = null;

                for (int j = 0; j < FieldList.Count; j++)
                {
                    ExportField vf = FieldList[j];
                    String cFieldName = vf.FieldName;
                    String cFieldValue = vf.FieldValue;
                    if (cFieldValue.Length > 0)
                    {
                        cFieldValue = Base64.Base64ToStr(cFieldValue);
                    }
                    DataRow[] drs = dtFieldList.Select("FIELD_NAME='" + cFieldName + "'");
                    if (String.IsNullOrEmpty(cFieldValue))
                    {
                        cFieldValue = "null";
                    }
                    else
                    {
                        cFieldValue = "'" + cFieldValue + "'";
                    }
                    if (cFieldList == null)
                    {
                        cFieldList = cFieldName;
                        cValueList = cFieldValue;
                    }
                    else
                    {
                        cFieldList = cFieldList + "," + cFieldName;
                        cValueList = cValueList + "," + cFieldValue;
                    }

                    if ((drs != null) && (drs.Length > 0))
                    {
                        int isKey = StringEx.getInt(drs[0]["iskey"]);
                        if (isKey > 0)
                        {
                            KeyField.Add(cFieldName);
                            if (cWhereParm == null)
                            {
                                cWhereParm = " (" + cFieldName + "=" + cFieldValue + ")";
                            }
                            else
                            {
                                cWhereParm = cWhereParm + " AND (" + cFieldName + "=" + cFieldValue + "') ";
                            }
                        }
                    }
                }
                sqls.Add(" DELETE FROM " + cTableName + " WHERE " + cWhereParm);
                sql.Append(" INSERT INTO " + cTableName + "(" + cFieldList + ") VALUES(" + cValueList + ")");

                for (int j = 0; j < FileList.Count; j++)
                {
                    sql.Clear();
                    ExportFile vf = FileList[j];
                    String cFileName = cRootDir + vf.Url.Replace("/", "\\");
                    bool isSaved = Base64.SaveBase64File(cFileName, vf.Data);
                    if (isSaved)
                    {
                        sqls.Add("DELETE FROM S_UPLOAD WHERE ID='" + vf.ID + "'");
                        sqls.Add(" INSERT INTO S_UPLOAD(ID,TEX,URL) VALUES('" + vf.ID + "','" + vf.Text + "','" + vf.Url + "')");
                    }
                }

                sqls.Add(sql.ToString());
            }
            return DbManager.ExecSQL(sqls);
        }
    }
}
