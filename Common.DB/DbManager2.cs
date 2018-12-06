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

            vret.dtrows = QueryData(sql.ToString());
            return vret;
        }

        /// <summary>
        /// GetDataTable获取
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable QueryData(string strSql)
        {
            IDBBASE vDB = DBFactory.getDB();
            return vDB.Query(strSql, null);
        }


        public static DataTable QueryData(string strSql, Object[] ParmList)
        {
            IDB vDB = DBFactory.getDB();
            return vDB.Query(strSql, ParmList);
        }

        /// <summary>
        /// 查询数据不分页
        /// </summary>
        /// <param name="cFileList">字段列表</param>
        /// <param name="cTableName">数据表</param>
        /// <param name="cWhereParm">条件</param>
        /// <param name="cOrderBy">排序</param>
        /// <returns></returns>
        public static DataTable QueryData(String cFileList, String cTableName, String cWhereParm, String cOrderBy)
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
            return QueryData(sql.ToString());
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="cFileList"></param>
        /// <param name="cTableName"></param>
        /// <param name="cWhereParm"></param>
        /// <returns></returns>
        public static DataTable QueryData(String cFileList, String cTableName, String cWhereParm)
        {
            return QueryData(cFileList, cTableName, cWhereParm, null);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="cFileList"></param>
        /// <param name="cTableName"></param>
        /// <returns></returns>
        public static DataTable QueryData(String cFileList, String cTableName)
        {
            return QueryData(cFileList, cTableName, null, null);
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
            DataTable dt = QueryData(strSql);
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
            return StringEx.getInt(sStr);
        }

        public static int ExeSql(List<String> sqlList)
        {
            return ExeSql(sqlList, null);
        }

        public static int ExeSql(string sql)
        {
            List<String> sqls = new List<string>();
            sqls.Add(sql);
            return ExeSql(sqls, null);
        }

        public static int ExeSql(String[] sqlList)
        {
            List<String> sqls = new List<string>();
            for (int i = 0; i < sqlList.Length; i++)
            {
                sqls.Add(StringEx.getString(sqlList[i]));
            }
            return ExeSql(sqls, null);
        }

        /// <summary>
        /// 执行多条SQL语句
        /// </summary>
        /// <param name="sqlArray"></param>
        /// <param name="callBy"></param>
        /// <returns></returns>
        public static int ExeSql(List<String> vSqlList, List<Object[]> vParmList)
        {
            IDBBASE vDB = DBFactory.getDB();
            return vDB.ExeSql(vSqlList, vParmList);
        }
    }
}