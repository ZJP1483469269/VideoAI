using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;

namespace TLKJ.DAO
{
    public class DB_Dao
    {
        public HttpRequest request = null;
        public HttpResponse response = null;
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public DataTable query(String sql)
        {
            return DbManager.QueryData(sql);
        }

        /// <summary>
        /// 省整改审批或退回
        /// </summary>
        /// <param name="cJCTB_GUID"></param>
        /// <param name="cUserID"></param>
        /// <param name="cDayTime"></param>
        /// <param name="cResult"></param>
        /// <param name="cReason"></param>
        /// <returns></returns>
        public int execsql(String sql)
        {
            int iCode = DbManager.ExecSQL(sql.ToString()); 
            return iCode;
        }
    }
}
