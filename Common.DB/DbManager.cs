using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TLKJ.DB;
using TLKJ.Utils;

namespace TLKJ.DB
{
    /// <summary>
    /// G_CN 的摘要说明.
    /// </summary>
    ///
    public class DbManager
    {
        public static bool Exists(string strSQL)
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    string obj = db.GetStrValue(strSQL);
                    int iResult = obj.Equals("") ? 0 : int.Parse(obj);
                    if (iResult > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    log4net.WriteLogFile("获取连接对象失败", 3);
                    return false;
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }
        }

        public static int GetMaxID(String StrFieldName, String StrTableName)
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    String strSQL = "SELECT MAX(" + StrFieldName + ") FROM " + StrTableName;
                    Object objValue = db.GetStrValue(strSQL);
                    if (objValue.Equals(""))
                    {
                        objValue = "0";
                    }
                    return int.Parse(objValue.ToString()) + 1;
                }
                else
                {
                    log4net.WriteLogFile("获取连接对象失败", 3);
                    return 0;
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }
        }


        public static String GetStrValue(string strSQL)
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    return db.GetStrValue(strSQL);
                }
                else
                {
                    log4net.WriteLogFile("G_CN:GetStrValue:获取连接对象失败", 3);
                    return "";
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }

        } 

        public static String ServerDateTime()
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    IDBBASE myface = (IDBBASE)db;
                    return myface.ServerDateTime();
                }
                else
                {
                    log4net.WriteLogFile("获取连接对象失败", 3);
                    return null;
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }
        }

        public static DataTable QueryData(string strSQL)
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    return db.GetDataTable(strSQL); ;
                }
                else
                {
                    log4net.WriteLogFile("获取连接对象失败", 3);
                    return null;
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }
        }


        public static int ExecSQL(string strSQL)
        {
            List<string> sqls = new List<string>();
            sqls.Add(strSQL);
            return ExecSQL(sqls, null);
        }

        public static int ExecSQL(List<String> sqls)
        {            
            return ExecSQL(sqls, null);
        }
        public static int ExecSQL(string[] sqlStrlist)
        {
            List<string> sqls = new List<string>();
            for (int i = 0; i < sqlStrlist.Length; i++)
            {
                sqls.Add(sqlStrlist[i]);
            }
            return ExecSQL(sqls, null);
        }


        public static DBResult Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            DBResult vret = new DBResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT count(1) FROM " + cTableName);
            if (!String.IsNullOrWhiteSpace(cWhereParm))
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

            sql.Clear();

            sql.Append(" SELECT * FROM ( ");
            sql.Append("    select row_number() over(" + cOrderBy + ") rownumber ");
            sql.Append("    ," + cFileList);
            sql.Append("    from " + cTableName);
            if (!String.IsNullOrWhiteSpace(cWhereParm))
            {
                sql.Append("    where " + cWhereParm);
            }
            sql.Append(" ) tab");
            sql.Append(" where rownumber>" + iStart + " and rownumber<" + iFinish);
            vret.dtrows = QueryData(sql.ToString());
            return vret;
        }

        public static int ExecSQL(List<string> sqls, List<object[]> parmList)
        {
            JDBBASE db = DbConnectionPool.Instance();
            try
            {
                if (db != null)
                {
                    try
                    {
                        IDBBASE vDB = (IDBBASE)db;
                        return vDB.ExecSQL(sqls, parmList);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile(ex.Message, 3);
                        return 0;
                    }
                }
                else
                {
                    log4net.WriteLogFile("获取连接对象失败", 3);
                    return -1;
                }
            }
            finally
            {
                DbConnectionPool.ReturnConnect(db);
            }
        }
    }
}
