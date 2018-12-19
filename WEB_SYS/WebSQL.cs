using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Data;
using TLKJ.SYS.WebSQL; 

namespace TLKJ_IVS
{
    public class WebDB
    {
        public static String GetStrValue(String vSQL)
        {
            WebDBSoapClient vClient = new WebDBSoapClient();
            return vClient.GetStrValue(vSQL);
        }


        public static DataTable QueryData(String vSQL)
        {
            WebDBSoapClient vClient = new WebDBSoapClient();
            return vClient.QueryData(vSQL);
        }


        public static DBResult Query(String cFileList, String cTableName, String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            WebDBSoapClient vClient = new WebDBSoapClient();
            return vClient.Query(cFileList, cTableName, cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        public static int ExecSQL(String vSQL)
        {
            List<String> sqls = new List<string>();
            sqls.Add(vSQL);
            return ExecSQL(sqls);
        }

        public static int ExecSQL(List<String> sqls)
        {
            return ExecSQL(sqls, null);
        }

        public static int ExecSQL(String[] sqls, Object[][] ParmList)
        {
            WebDBSoapClient vClient = new WebDBSoapClient();
            return vClient.ExecSQL(sqls, ParmList);
        }

        public static int ExecSQL(List<String> sqlList, List<Object[]> parmList)
        {
            WebDBSoapClient vClient = new WebDBSoapClient();
            Object[][] Parms = new Object[parmList.Count][];
            String[] sqls = new String[parmList.Count];
            for (int i = 0; i < parmList.Count; i++)
            {
                Parms[i] = parmList[i];
                sqls[i] = sqlList[i];
            }

            return vClient.ExecSQL(sqls, Parms);
        }
    }
}
