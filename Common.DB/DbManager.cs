using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Data.Common;
using TLKJ.Utils;

namespace TLKJ.DB
{
    /// <summary>
    /// DbManager 的摘要说明
    /// </summary>
    public class DbManager
    {
        public DbManager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="cFileList"></param>
        /// <param name="cTableName"></param>
        /// <param name="cWhereParm"></param>
        /// <param name="cOrderBy"></param>
        /// <param name="iPageNo"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static DBResult Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            DBResult vret = new DBResult();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT count(1) FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            int iRowCount = StringEx.getInt(GetValue(sql.ToString()));
            int iPageCount = iRowCount / iPageSize;
            int iPageLeft = iRowCount % iPageSize;
            if (iPageLeft != 0)
            {
                iPageCount = iPageCount + 1;
            }
            if (iPageNo > iPageCount)
            {
                iPageNo = iPageCount;
            }
            if (iPageNo <= 0)
            {
                iPageNo = 1;
            }
            if (!String.IsNullOrEmpty(cOrderBy))
            {
                if (!cOrderBy.Trim().ToUpper().StartsWith("ORDER BY"))
                {
                    cOrderBy = " ORDER BY " + cOrderBy;
                }
            }
            int iStart = (iPageNo - 1) * iPageSize;
            int iFinish = iPageNo * iPageSize;
            vret.PAGE_SIZE = iPageSize;
            vret.ROW_COUNT = iRowCount;
            vret.PAGE_NO = iPageNo;
            vret.PAGE_COUNT = iPageCount;

            sql.Length = 0;

            sql.Append(" SELECT * FROM ( ");
            sql.Append("    select row_number() over(" + cOrderBy + ") rownumber ");
            sql.Append("    ," + cFileList);
            sql.Append("    from " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            sql.Append(" ) tab");
            sql.Append(" where rownumber>" + iStart + " and rownumber<=" + iFinish);

            vret.dtrows = GetDataTable(sql.ToString());
            return vret;
        }

        /// <summary>
        /// GetDataTable获取
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string strSql)
        {
            return (new DbManager().QueryTable(strSql));
        }

        /// <summary>
        /// 查询数据不分页
        /// </summary>
        /// <param name="cFileList">字段列表</param>
        /// <param name="cTableName">数据表</param>
        /// <param name="cWhereParm">条件</param>
        /// <param name="cOrderBy">排序</param>
        /// <returns></returns>
        public static DataTable GetDataTable(String cFileList, String cTableName, String cWhereParm, String cOrderBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT " + cFileList);
            sql.Append(" FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append(" where " + cWhereParm);
            }
            if (!String.IsNullOrEmpty(cOrderBy))
            {
                sql.Append(" order by " + cOrderBy);
            }
            return GetDataTable(sql.ToString());
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="cFileList"></param>
        /// <param name="cTableName"></param>
        /// <param name="cWhereParm"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(String cFileList, String cTableName, String cWhereParm)
        {
            return GetDataTable(cFileList, cTableName, cWhereParm, null);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="cFileList"></param>
        /// <param name="cTableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(String cFileList, String cTableName)
        {
            return GetDataTable(cFileList, cTableName, null, null);
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable QueryTable(string strSql)
        {
            strSql = (strSql == null) ? "" : strSql.Trim();

            if (strSql.Equals("")) return null;
            SqlConnection cn = new SqlConnection(INIConfig.ReadString("Config", "ConnStr"));
            if (cn == null)
            {
                return null;
            }
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog(ex.Message);
            }
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = strSql;
                SqlDataAdapter dap = new SqlDataAdapter();
                dap.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dap.Fill(ds);
                if ((ds != null) && (ds.Tables.Count > 0))
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("SQL：" + ex.Message);
                log4net.WriteTextLog("执行：" + strSql);
                return null;
            }
            finally
            {
                cn.Close();
            }
        }
        /// <summary>
        /// 获取指定sqL的值
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="callBy"></param>
        /// <returns></returns>
        public static String GetValue(string strSql)
        {
            string strTemp = null;
            strSql = (strSql == null) ? "" : strSql.Trim();
            if (strSql.Equals("")) return null;
            DataTable dt = GetDataTable(strSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                strTemp = StringEx.getString(dt.Rows[0][0]);
                return strTemp.ToString();
            }
            else
            {
                return "";
            }
        }

        public static String GetDate()
        {
            string cDayTime = GetDateTime();
            return cDayTime.Substring(0, cDayTime.IndexOf(" "));
        }

        public static String GetDateTime()
        {
            string strSql = " SELECT GETDATE() ";
            return GetValue(strSql);
        }
        ///
        public static int GetCount(string strSql)
        {
            string sStr = GetValue(strSql);
            if (string.IsNullOrEmpty(sStr))
            {
                return 0;
            }
            else
            {
                try
                {
                    return int.Parse(sStr);
                }
                catch (Exception ex)
                {
                    return AppConfig.FAILURE;
                }
            }
        }

        public static ExecSqlResult ExeSql(List<String> sqlList)
        {
            string[] sqls = new string[sqlList.Count];
            for (int i = 0; i < sqlList.Count; i++)
            {
                sqls[i] = sqlList[i];
            }
            return ExeSql(sqls, null);
        }

        public static ExecSqlResult ExeSql(string sql)
        {
            string[] sqls = new string[1];
            sqls[0] = sql;
            return ExeSql(sqls, null);
        }

        public static ExecSqlResult ExeSql(string[] sqlList)
        {
            string[] sqls = new string[sqlList.Length];
            for (int i = 0; i < sqlList.Length; i++)
            {
                sqls[i] = StringEx.getString(sqlList[i]);
            }
            return ExeSql(sqls, null);
        }

        public static ExecSqlResult ExeSql(string sql, DbParameter[] ParmList)
        {
            string[] sqls = new string[1];
            sqls[0] = sql;
            List<DbParameter[]> vParmList = new List<DbParameter[]>(1);
            vParmList.Add(ParmList);
            return ExeSql(sqls, vParmList);
        }

        /// <summary>
        /// 执行多条SQL语句
        /// </summary>
        /// <param name="sqlArray"></param>
        /// <param name="callBy"></param>
        /// <returns></returns>
        public static ExecSqlResult ExeSql(string[] vSqlList, List<DbParameter[]> vParmList)
        {
            ExecSqlResult result = new ExecSqlResult();
            int iSqlCount = 0;
            SqlConnection cn = null;
            SqlTransaction trans = null;
            string sql = "";
            try
            {
                cn = new SqlConnection(INIConfig.ReadString("Config", "ConnStr"));
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                trans = cn.BeginTransaction();
                cmd.Transaction = trans;
                string Trandid = Guid.NewGuid().ToString();
                for (int i = 0; i < vSqlList.Length; i++)
                {
                    sql = (vSqlList[i] == null) ? "" : vSqlList[i].ToString().Trim();
                    if (sql.Length > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = sql;
                        if (vParmList != null)
                        {
                            if (vParmList[i] != null)
                            {
                                for (int j = 0; j < vParmList[i].Length; j++)
                                {
                                    cmd.Parameters.Add(vParmList[i][j]);
                                }
                            }
                        }
                        int flag = cmd.ExecuteNonQuery();
                        if (flag > 0)
                        {
                            iSqlCount++;
                        }

                    }
                }
                trans.Commit();
                trans = null;
                result.Code = iSqlCount;
                return result;
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("SQL：" + ex.Message);
                log4net.WriteTextLog("执行：" + sql);

                #region
                if (trans != null) trans.Rollback();
                #endregion
                result.Code = -1;
                result.Message = ex.Message;
                return result;
            }
            finally
            {
                if (cn != null)
                {
                    try
                    {
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        cn = null;
                    }
                }
            }
        }
        public static string DataTable2Json(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }


        public static DataSet GetDataSet(string connectionString, string safeSql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(safeSql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
        }
    }
}