using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using System.Collections;
using TLKJ.Utils;

namespace TLKJ.DB
{
    public abstract class JDBBASE
    {
        protected DbConnection dbConnect;
        public int DBType;
        public int _IsDebug = 0;
        public abstract void CloseConnect();
        public bool Connectd
        {
            get
            {
                if (dbConnect == null)
                {
                    return false;
                }
                if (dbConnect.State != ConnectionState.Open)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public int ExecSQL(ArrayList sqlList)
        {
            String[] sqls = new string[sqlList.Count];

            string strSQL = "";
            for (int i = 0; i < sqlList.Count; i++)
            {
                string str = sqlList[i].ToString();
                strSQL = (str == null) ? "" : str.Trim();
                if (strSQL.Length > 0)
                {
                    sqls[i] = strSQL;
                }
            }
            return ExecSQL(sqls);
        }

        public int ExecSQL(String[] sqls)
        {
            DbCommand Cmd = dbConnect.CreateCommand();
            DbTransaction trans = dbConnect.BeginTransaction();
            Cmd.Transaction = trans;
            string strSQL = "";
            try
            {
                for (int i = 0; i < sqls.Length; i++)
                {
                    string str = sqls[i].ToString();
                    strSQL = (str == null) ? "" : str.Trim();
                    if (strSQL.Length > 0)
                    {
                        Cmd.CommandText = strSQL;
                        Cmd.ExecuteNonQuery();
                        log4net.WriteLogFile("ִ�����ݲ�����" + strSQL);
                    }
                }
                trans.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                log4net.WriteLogFile(strSQL, 1);
                log4net.WriteLogFile("ִ��SQL�����쳣:" + ex.Message, 3);
                return -1;
            }
        }
        public DataTable GetDataTable(String sql)
        {
            DbCommand Cmd = dbConnect.CreateCommand();
            Cmd.CommandText = sql;
            try
            {
                IDataReader dr = Cmd.ExecuteReader();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Tables.Add(dt);
                ds.Load(dr, LoadOption.OverwriteChanges, dt);
                ds.EnforceConstraints = false;
                try
                {
                    dr.Close();
                }
                catch (Exception ex)
                {

                }
                return dt;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(sql, 1);
                log4net.WriteLogFile("ִ��SQL�����쳣:" + ex.Message, 3);
                return null;
            }
        }

        public object GetValue(string strSQL)
        {
            if (strSQL.Trim().Equals(""))
                return null;
            try
            {
                DataTable dt = GetDataTable(strSQL);
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message, 1);
                log4net.WriteLogFile("ִ�����" + strSQL, 3);
                return null;
            }
        }

        public String GetStrValue(string strSQL)
        {
            if (strSQL.Trim().Equals(""))
                return null;
            try
            {
                Object obj = GetValue(strSQL);
                if ((obj != null) && (obj != System.DBNull.Value))
                    return obj.ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message, 3);
                return null;
            }
        }

        public int ExecSQL(string sql)
        {
            DbCommand Cmd = dbConnect.CreateCommand();
            Cmd.CommandText = sql;
            try
            {
                int iCode = Cmd.ExecuteNonQuery();
                log4net.WriteLogFile("ִ�����ݲ���:" + sql);
                return iCode;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(sql, 1);
                log4net.WriteLogFile("ִ��SQL�����쳣:" + ex.Message, 3);
                return -1;
            }
        }
    }
}

