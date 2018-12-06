using System;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using TLKJ.Utils;

namespace TLKJ.DB
{
    /// <summary>
    /// DataAccess 的摘要说明。


    /// </summary>
    public class DBOledb : JDBBASE, IDBBASE
    {
        private OleDbConnection Connect;
        public DBOledb(String AConnString)
        {
            if (dbConnect == null)
            {
                dbConnect = new OleDbConnection();
                dbConnect.ConnectionString = AConnString;
                Connect = (OleDbConnection)(dbConnect);
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
        public DataTable QueryData(string strSQL, string cOrderBy, int iPageNo, int iPageSize)
        {
            try
            {
                OleDbCommand Cmd = Connect.CreateCommand();
                OleDbDataAdapter sqldt = new OleDbDataAdapter();
                sqldt.SelectCommand = Cmd;

                if (!String.IsNullOrEmpty(cOrderBy))
                {
                    strSQL = strSQL + " " + cOrderBy;
                }

                if (iPageSize > 0)
                {
                    int iStart = (iPageNo - 1) * iPageSize;
                    if (iStart < 0)
                    {
                        iStart = 0;
                    }

                    strSQL = strSQL + " limit " + iStart + "," + iPageSize;
                }

                Cmd.CommandText = strSQL;
                DataTable dt = new DataTable();
                sqldt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(strSQL);
                log4net.WriteLogFile("执行获取数据集:" + ex.Message);
                return null;
            } 
        }
        public ArrayList GetRows(string strSQL)
        {
            ArrayList dbrows = new ArrayList();
            try
            {
                DataTable dtRow = this.GetDataTable(strSQL);
                if (dtRow != null)
                {
                    for (int i = 0; i < dtRow.Rows.Count; i++)
                    {
                        Hashtable row = new Hashtable();
                        for (int j = 0; j < dtRow.Columns.Count; j++)
                        {
                            string StrFieldName = dtRow.Columns[j].ColumnName;
                            string StrFieldValue = null;
                            if (dtRow.Rows[i][j] != System.DBNull.Value)
                                StrFieldValue = dtRow.Rows[i][j].ToString();
                            row.Add(StrFieldName, StrFieldValue);
                        }
                        dbrows.Add(row);
                    }
                }
                return dbrows;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(strSQL, 1);
                log4net.WriteLogFile("执行获取数据集:" + ex.Message, 3);
                return null;
            }
        }
         

        ~DBOledb()
        {
            //
        }

        public override void CloseConnect()
        {
            try
            {
                Connect.Close();
                Connect = null;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("关闭连接失败:" + ex.Message, 3);
            }
        }

        public int ExecProc(string StrProcName, ArrayList Parms)
        {
            try
            {
                OleDbCommand Cmd = new OleDbCommand(StrProcName, Connect);
                Cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Parms.Count; i++)
                {
                    Cmd.Parameters.Add(Parms[0].ToString());
                }
                return Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(StrProcName + ":" + Parms.ToString(), 1);
                log4net.WriteLogFile("执行存储过程异常:" + ex.Message, 3);
                return 0;
            }
        }

        public String SysDateTime(String dtValue)
        {
            string strValue = "null";
            DateTime dtTime;
            try
            {
                dtTime = DateTime.Parse(dtValue);
                if (dtValue.EndsWith("0:00:00"))
                {
                    strValue = " to_date('" + dtTime.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')";
                }
                else if (dtValue.StartsWith("1899-12-30"))
                {
                    strValue = " to_date('" + dtTime.ToString("hh:mm:ss") + "','hh24:mi:ss')";
                }
                else
                {
                    strValue = " to_date('" + dtTime.ToString("yyyy-MM-dd hh:mm:ss") + "','yyyy-MM-dd hh24:mi:ss')";
                }
            }
            catch
            {
                ;
            }
            return strValue;
        }

        public String DateKey()
        {
            return "sysdate";
        }

        public String ServerDateTime()
        {
            string StrDtValue = GetStrValue("select sysdate from dual");
            return DateTime.Parse(StrDtValue).ToString("yyyy-MM-dd hh:mm:ss");
        }

       

        /*****************************************************************
        ** 函数名：GetDataSet
        ** 输 入：strSql
        ** strSql：需要执行的sql
        ** 返 回：DataSet
        ** 功能描述：执行查询sql，返回DataSet
        ****************************************************************/
        public DataSet GetDataSet(string strSql)
        {
            try
            {
                log4net.WriteLogFile(strSql, 1);
                DataSet SqlDs = new DataSet();
                OleDbCommand Cmd = Connect.CreateCommand();
                OleDbDataAdapter sqldt = new OleDbDataAdapter();
                sqldt.SelectCommand = Cmd;
                Cmd.CommandText = strSql;
                sqldt.Fill(SqlDs);
                return SqlDs;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("执行SQL语句:" + strSql, 1);
                log4net.WriteLogFile(ex.Message, 3);
                return null;
            }
        }
          
        public bool ExecSQL(String strSQL, string[] sqlStrList, String ExplainField)
        {
            bool Flag = false;
            OleDbCommand Cmd = Connect.CreateCommand();
            OleDbTransaction trans = Connect.BeginTransaction();
            Cmd.Transaction = trans;
            try
            {
                Cmd.CommandText = strSQL;
                Object objValue = Cmd.ExecuteScalar();

                if (objValue != null)
                {
                    Flag = true;
                    for (int i = 0; i < sqlStrList.Length; i++)
                    {
                        string str = sqlStrList[i].ToString();
                        String sql = (str == null) ? "" : str.Trim();
                        if (sql.TrimEnd().Length > 0)
                        {
                            try
                            {
                                sql = sql.Replace(ExplainField, objValue.ToString());
                                Cmd.CommandText = sql;
                                Cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                log4net.WriteLogFile("执行SQL语句失败:" + sql, 1);
                                log4net.WriteLogFile(ex.Message, 3);
                                Flag = false;
                                break;
                            }
                        }
                    }
                }

                if (Flag == false)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();
                }
            }
            finally
            {
                CloseConnect();
            }
            return Flag;
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}

