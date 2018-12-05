using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Data.Common;

namespace TLKJ.DB
{
    /// <summary>
    /// IDB 的摘要说明
    /// </summary>
    public interface IDB
    {
        DBResult QueryData(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize);

        int ExeSql(List<String> sqls, List<Object[]> ParmList);

        DataTable Query(String sql, Object[] ParmList);
        DbConnection getDBConnect();
    }
}