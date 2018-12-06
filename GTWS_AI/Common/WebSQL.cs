using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLKJ.Utils;
using System.Data;
using TLKJ.WebSys;

namespace TLKJ_IVS
{
    public class WebSQL
    {
        public int ExecSQL(String[] sqls)
        {
            String cUrl = Config.GetAppSettings(AppConfig.WEB_URL) + "api/rest.ashx";
            String cActionType = "SQLDB";
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            Dictionary<String, String> ParmList = new Dictionary<string, string>();
            ParmList.Add("action_type", cActionType);
            ParmList.Add("action_method", "execute");

            for (int i = 0; i < sqls.Length; i++)
            {
                string sql = sqls[i];
                if (!string.IsNullOrEmpty(sql))
                {
                    ParmList.Add("SQL_" + i, sql);
                }
            }
            HttpUtil vTool = new HttpUtil();
            string cValue = vTool.HttpPost(cUrl, ParmList);
            if (cValue != null)
            {
                return StringEx.getInt(cValue);
            }
            else
            {
                return AppConfig.FAILURE;
            }

        }

        /// <summary>
        /// 执行SQL 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecSQL(String sql)
        {
            string[] sqls = new string[1];
            sqls[0] = sql;
            return ExecSQL(sqls);
        }

        /// <summary>
        /// 根据SQL语句查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable Query(String sql)
        {
            String cUrl = Config.GetAppSettings(AppConfig.WEB_URL) + "api/rest.ashx";
            String cActionType = "SQLDB";
            DataTable dtRows = null;
            Dictionary<String, String> ParmList = new Dictionary<string, string>();
            ParmList.Add("action_type", cActionType);
            ParmList.Add("action_method", "query");
            ParmList.Add("SQL", sql);
             
            HttpUtil vTool = new HttpUtil();
            string cValue = vTool.HttpPost(cUrl, ParmList);
            try
            {
                dtRows = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(cValue);
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog(ex.Message);
            }
            return dtRows;
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public String getStrValue(String sql)
        {
            DataTable dtRows = Query(sql);
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                return null;
            }
            else
            {
                return StringEx.getString(dtRows.Rows[0][0]);
            }
        }
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="cFieldList">字段列表</param>
        /// <param name="cTableName">数据表</param>
        /// <param name="cWhereParm">条件参数</param>
        /// <param name="cOrderParm">排序参数</param>
        /// <returns></returns>
        public DataTable Query(String cFieldList, String cTableName, String cWhereParm, String cOrderParm)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            if (String.IsNullOrEmpty(cFieldList))
            {
                sql.Append(" * ");
            }
            else
            {
                sql.Append(" " + cFieldList + " ");
            }


            if (!String.IsNullOrEmpty(cTableName))
            {
                sql.Append(" FROM " + cTableName);
            }

            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append(" WHERE " + cWhereParm);
            }

            if (!String.IsNullOrEmpty(cOrderParm))
            {
                sql.Append(" ORDER BY " + cOrderParm);
            }
            return Query(sql.ToString());
        }
    }
}
