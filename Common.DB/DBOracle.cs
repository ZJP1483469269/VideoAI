using System;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using TLKJ.Utils;

namespace TLKJ.DB
{
    /// <summary>
    /// DataAccess 的摘要说明。 
    /// </summary>
    public class DBOracle : JDBBASE, IDBBASE
    {
        private OracleConnection Connect;
        public DBOracle(String AConnString)
        {
            if (dbConnect == null)
            {
                dbConnect = new OracleConnection();
                dbConnect.ConnectionString = AConnString;
                Connect = (OracleConnection)(dbConnect);
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


        /// <summary>
        /// 执行一个存储过程,并返回它受影响的条数
        /// </summary>
        /// <param name="storedProcName">过程的名字</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>数据库中受影响的数据行数</returns>
        public int RunProcedure(IDataParameter[] parameters, string storedProcName)
        {
            int result;
            OracleCommand command = new OracleCommand(storedProcName, Connect);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            result = command.ExecuteNonQuery();
            return result;
        }



        ~DBOracle()
        {
            //
        }
        public DataTable QueryData(string strSQL, string cOrderBy, int iPageNo, int iPageSize)
        {
            if (iPageNo <= 0)
                iPageNo = 1;

            int startRecord = iPageSize * (iPageNo - 1) + 1;
            int endRecord = startRecord + (iPageSize - 1);

            System.Text.StringBuilder sbSql = new System.Text.StringBuilder();
            sbSql.Append("select b.* from (");
            sbSql.Append(" select rownum as rowIdx,a.* from (");
            sbSql.Append(strSQL);

            sbSql.Append(" ) a where rownum <=" + endRecord);
            sbSql.Append(" ) b where rowIdx >=" + startRecord);

            return GetDataTable(sbSql.ToString());
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
            string StrValues = "";
            try
            {
                OracleCommand Cmd = new OracleCommand(StrProcName, Connect);
                Cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Parms.Count; i++)
                {
                    JValue myValue = (JValue)(Parms[i]);
                    string AValue = myValue.KeyValue;
                    OracleParameter parm = null;
                    if (JValidate.IsNumeric(AValue))
                        parm = new OracleParameter(myValue.KeyName, System.Data.OracleClient.OracleType.Int32);
                    else
                        parm = new OracleParameter(myValue.KeyName, System.Data.OracleClient.OracleType.VarChar, AValue.Length);
                    parm.Value = AValue;
                    StrValues = StrValues + "|" + AValue.ToString();
                    parm.Direction = ParameterDirection.Input;
                    Cmd.Parameters.Add(parm);
                }
                return Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(StrProcName + ":" + StrValues, 1);
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
                    strValue = " to_date('" + dtTime.ToString("HH:mm:ss") + "','hh24:mi:ss')";
                }
                else
                {
                    strValue = " to_date('" + dtTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-dd hh24:mi:ss')";
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
            return DateTime.Parse(StrDtValue).ToString("yyyy-MM-dd HH:mm:ss");
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
                OracleCommand Cmd = Connect.CreateCommand();
                OracleDataAdapter sqldt = new OracleDataAdapter();
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
            OracleCommand Cmd = Connect.CreateCommand();
            OracleTransaction trans = Connect.BeginTransaction();
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

