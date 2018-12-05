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
    public class DBFactory
    {
        public DBFactory()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static IDB getDB()
        {
            String cDBType = Config.GetAppSettings("DBTYPE");
            if (cDBType.ToUpper().Equals("MSSQL"))
            {
                return new MsSqlDB();
            }            
            else
            {
                return null;
            }
        }
    }
}