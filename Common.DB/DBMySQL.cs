using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using TLKJ.Utils;
using MySql.Data.MySqlClient;

namespace TLKJ.DB
{
    /// <summary>
    /// DBSqlServer 的摘要说明。

    /// </summary>
    public class DBMySQL : JDBBASE, IDBBASE
    {

        public DBMySQL(String AConnString)
        {

            if (dbConnect == null)
            {
                dbConnect = new MySqlConnection();
                dbConnect.ConnectionString = AConnString;
                dbConnect = (MySqlConnection)(dbConnect);
                try
                {
                    dbConnect.Open();
                }
                catch (Exception ex)
                {
                    log4net.WriteLogFile(AConnString, 1);
                    log4net.WriteLogFile("连接数据库异常:" + ex.Message, 3);
                }
            }
        }

        public DataTable Query(String strSQL)
        {
            try
            {
                DataSet SqlDs = new DataSet();
                DbCommand Cmd = dbConnect.CreateCommand();
                DbDataAdapter sqldt = new MySqlDataAdapter();
                sqldt.SelectCommand = Cmd;
                Cmd.CommandText = strSQL;
                sqldt.Fill(SqlDs);
                return SqlDs.Tables[0];
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("GetDataSet" + strSQL, 1);
                log4net.WriteLogFile(ex.Message, 3);
                return null;
            }
        }
        public DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            DBResult vret = new DBResult();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT count(1) FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            int iRowCount = StringEx.getInt(GetStrValue(sql.ToString()));
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

            DataTable dtRows = Query(sql.ToString());
            return dtRows;
        }

        public DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy)
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
            return Query(sql.ToString());
        }

        public DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo)
        {
            StringBuilder sql = new StringBuilder();
            if (iPageNo > 0)
            {
                sql.Append(" SELECT " + cFileList);
            }

            sql.Append(" FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append(" where " + cWhereParm);
            }
            if (!String.IsNullOrEmpty(cOrderBy))
            {
                sql.Append(" order by " + cOrderBy);
            }
            if (iPageNo > 0)
            {
                sql.Append(" LIMIT " + iPageNo);
            }
            return Query(sql.ToString());
        }

        public String ServerDateTime()
        {
            string StrDtValue = GetStrValue("select now()");
            return DateTime.Parse(StrDtValue).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public DBResult QueryData(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            DBResult vret = new DBResult();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT count(1) FROM " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            int iRowCount = StringEx.getInt(GetStrValue(sql.ToString()));
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

            sql.Append(" SELECT " + cFileList);
            sql.Append("    from " + cTableName);
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            sql.Append(" ORDER BY " + cOrderBy);
            sql.Append(" LIMIT " + iPageSize + ",OFFSET " + iStart);
            vret.dtrows = Query(sql.ToString());
            return vret;
        }

        public int ExecSQL(List<String> sqls, List<Object[]> ParmList)
        {
            int iSqlCount = 0;
            MySqlTransaction trans = null;
            string sql = "";
            try
            {
                MySqlConnection cn = (MySqlConnection)dbConnect;
                MySqlCommand cmd = cn.CreateCommand();
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
                log4net.WriteLogFile("SQL：" + ex.Message);
                log4net.WriteLogFile("执行：" + sql);

                #region
                if (trans != null) trans.Rollback();
                #endregion
                return -1;
            }
        } 
        ~DBMySQL()
        {
            //
        }

        public override void CloseConnect()
        {
            try
            {
                dbConnect.Close();
                dbConnect = null;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("数据库关闭异常:" + ex.Message, 3);
            }
        }
    }
}

