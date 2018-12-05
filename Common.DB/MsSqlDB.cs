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
    /// MsSqlDB 的摘要说明
    /// </summary>
    public class MsSqlDB : IDB
    {
        public MsSqlDB()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable Query(string strSql)
        {
            return Query(strSql, null);
        }
        public String getStrValue(string strSql)
        {
            DataTable dtRows = Query(strSql);
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                return StringEx.getString(dtRows, 0, 0);
            }
            else
            {
                return String.Empty;
            }
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
        public DBResult QueryData(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            DBResult vret = new DBResult();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT count(1) FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            int iRowCount = StringEx.getInt(getStrValue(sql.ToString()));
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
            sql.Append(" where rownumber>" + iStart + " and rownumber<" + iFinish);

            vret.dtrows = Query(sql.ToString(), null);
            return vret;
        }

        public int ExeSql(List<String> sqls, List<Object[]> ParmList)
        {
            int iSqlCount = 0;  
            SqlTransaction trans = null;
            string sql = "";
            try
            {
                SqlConnection cn = (SqlConnection)getDBConnect();
                SqlCommand cmd = cn.CreateCommand();
                trans = cn.BeginTransaction();
                cmd.Transaction = trans;
                string Trandid = Guid.NewGuid().ToString();
                for (int i = 0; i < sqls.Count; i++)
                {
                    sql = (sqls[i] == null) ? "" : sqls[i].ToString().Trim();
                    if (sql.Length > 0)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = sql;
                        if (ParmList != null)
                        {
                            if (ParmList[i] != null)
                            {
                                for (int j = 0; j < ParmList[i].Length; j++)
                                {
                                    cmd.Parameters.Add(ParmList[i][j]);
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
                return iSqlCount;
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("SQL：" + ex.Message);
                log4net.WriteTextLog("执行：" + sql);

                #region
                if (trans != null) trans.Rollback();
                #endregion
                return -1;
            }
            finally
            {
                cn.Close();
            }
        }
        SqlConnection cn = null;
        public DbConnection getDBConnect()
        {
            String ConnStr = Config.GetAppSettings("ConnStr");
            if (cn == null)
            {
                cn = new SqlConnection(ConnStr);
            }
            try
            {

                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("SQL：" + ex.Message);
                cn = null;
                return null;
            }
        }
        public DataTable Query(String sql, Object[] ParmList)
        {
            try
            {
                SqlConnection cn = (SqlConnection)getDBConnect();
                SqlCommand cmd = cn.CreateCommand();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                cmd.CommandText = sql;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("SQL：" + ex.Message);
                log4net.WriteTextLog("执行：" + sql);
                return null;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}