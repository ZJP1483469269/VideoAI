using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

namespace TLKJ.DB
{
    /// <summary>
    /// IDBBASE ��ժҪ˵����
    /// </summary>
    /// 
    public interface IDBBASE
    {
        DataTable Query(String strSQL);

        DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy);

        DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo);

        DataTable Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize);

        String ServerDateTime();

        DBResult QueryData(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize);

        int ExecSQL(List<String> sqls, List<Object[]> ParmList);
    }
}
