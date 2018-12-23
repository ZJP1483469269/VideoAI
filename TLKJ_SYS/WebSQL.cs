using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TLKJ.Utils;

namespace TLKJ_IVS
{
    public class WebSQL
    {
        public static String GetStrValue(String vSQL)
        {
            String cWebUrl = INIConfig.ReadString("Config", "WEB_URL");
            String cUrl = "http://" + cWebUrl + "/api/rest.ashx";
            try
            {
                String cActionType = "SQLDB";
                String cActionMethod = "GetStrValue";
                cUrl = cUrl + "?action_type=" + cActionType + "&action_method=" + cActionMethod;
                HttpUtil vPost = new HttpUtil();
                Dictionary<String, String> vData = new Dictionary<string, string>();
                vData.Add("SQL", vSQL);
                String cStr = vPost.HttpPost(cUrl, vData);
                ActiveResult vret = JsonLib.ToObject<ActiveResult>(cStr);
                if (vret.result == AppConfig.SUCCESS)
                {
                    return StringEx.getString(vret.info);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public static DataTable QueryData(String vSQL)
        {
            String cWebUrl = INIConfig.ReadString("Config", "WEB_URL");
            String cUrl = "http://" + cWebUrl + "/api/rest.ashx";
            try
            {
                String cActionType = "SQLDB";
                String cActionMethod = "QueryData";
                cUrl = cUrl + "?action_type=" + cActionType + "&action_method=" + cActionMethod;
                HttpUtil vPost = new HttpUtil();
                Dictionary<String, String> vData = new Dictionary<string, string>();
                vData.Add("SQL", vSQL);
                String cStr = vPost.HttpPost(cUrl, vData);
                ActiveResult vret = JsonLib.ToObject<ActiveResult>(cStr);
                if (vret.result == AppConfig.SUCCESS)
                {
                    cStr = StringEx.getString(vret.info);
                    DataTable dtRows = JsonLib.ToObject<DataTable>(cStr);
                    return dtRows;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static DBResult Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {

            String cWebUrl = INIConfig.ReadString("Config", "WEB_URL");
            String cUrl = "http://" + cWebUrl + "/api/rest.ashx";
            try
            {
                String cActionType = "SQLDB";
                String cActionMethod = "Query";
                cUrl = cUrl + "?action_type=" + cActionType + "&action_method=" + cActionMethod;
                HttpUtil vPost = new HttpUtil();
                Dictionary<String, String> vData = new Dictionary<string, string>();
                vData.Add("FILELIST", cFileList);
                vData.Add("TABLENAME", cTableName);
                vData.Add("WHEREPARM", cWhereParm);
                vData.Add("ORDERBY", cOrderBy);
                vData.Add("PAGENO", StringEx.getString(iPageNo));
                vData.Add("PAGESIZE", StringEx.getString(iPageSize));

                String cStr = vPost.HttpPost(cUrl, vData);
                ActiveResult vret = JsonLib.ToObject<ActiveResult>(cStr);
                if (vret.result == AppConfig.SUCCESS)
                {
                    cStr = StringEx.getString(vret.info);
                    DataTable dtRows = JsonLib.ToObject<DataTable>(cStr);
                    DBResult rs = new DBResult();
                    rs.dtrows = dtRows;
                    rs.ROW_COUNT = vret.total;
                    return rs;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int ExecSQL(String vSQL)
        {
            List<String> sqls = new List<string>();
            sqls.Add(vSQL);
            return ExecSQL(sqls);
        }

        public static int ExecSQL(List<String> sqls)
        {
            return ExecSQL(sqls, null);
        }

        public static int ExecSQL(String[] sqls, Object[][] ParmList)
        {
            List<String> sqlList = new List<string>();
            List<Object[]> parmList = new List<object[]>();
            for (int i = 0; i < sqls.Length; i++)
            {
                sqlList.Add(sqls[i]);
                parmList.Add(ParmList[i]);
            }
            return ExecSQL(sqlList, parmList);
        }

        public static int ExecSQL(List<String> sqlList, List<Object[]> parmList)
        {
            String cWebUrl = INIConfig.ReadString("Config", "WEB_URL");
            String cUrl = "http://" + cWebUrl + "/api/rest.ashx";
            try
            {
                String cActionType = "SQLDB";
                String cActionMethod = "ExecSQL";
                cUrl = cUrl + "?action_type=" + cActionType + "&action_method=" + cActionMethod;
                HttpUtil vPost = new HttpUtil();
                Dictionary<String, String> vData = new Dictionary<string, string>();
                for (int i = 0; i < sqlList.Count; i++)
                {
                    String vSQL = sqlList[i];
                    vData.Add("SQL_" + (i + 1), vSQL);
                    if (parmList != null)
                    {
                        String cParm = JsonLib.ToJSON(parmList[i]);
                        vData.Add("PARM_" + (i + 1), cParm);
                    }
                }
                String cStr = vPost.HttpPost(cUrl, vData);
                ActiveResult vret = JsonLib.ToObject<ActiveResult>(cStr);
                return vret.result;
            }
            catch (Exception ex)
            {
                return AppConfig.FAILURE;
            }
        }
    }
}
