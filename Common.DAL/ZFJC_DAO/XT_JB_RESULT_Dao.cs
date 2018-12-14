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
    public class XT_JB_RESULT_Dao : BaseDao<XT_JB_RESULT>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DataTable Query(String cOPENID)
        {
            string sql = "SELECT *,(SELECT RESULT FROM XT_JB_RESULT X WHERE X.ID=T.ID) AS RESULT from XT_JB T where OPEN_ID= '" + cOPENID + "'";
             log4net.WriteLogFile(sql);
            return DbManager.QueryData(sql);
        }

        public XT_JB_RESULT FindOne(String cKeyID)
        {
            XT_JB_RESULT vo = new XT_JB_RESULT();
            string sql = "SELECT * FROM XT_JB_RESULT WHERE ID= '" + cKeyID + "'";
             log4net.WriteLogFile(sql);
            DataTable dtRows = DbManager.QueryData(sql);
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = (XT_JB_RESULT)ReadDB(vo, dtRows.Rows[0]);
            }
            return vo;

        }
    }
}
