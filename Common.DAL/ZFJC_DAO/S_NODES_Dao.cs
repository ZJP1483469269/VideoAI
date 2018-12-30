using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TLKJ.Utils;
using System.Data;
using System.Web;
using TLKJ.DB;

namespace TLKJ.DAO
{
    public class S_NODES_Dao : BaseDao<S_NODES>
    {
        DataTable dtRows = null;
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DataTable QueryList(String cRoleID, String cTypeID)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "SELECT * FROM S_NODES T "
                    + " WHERE (T.ISACTIVE=1) "
                    + " AND (TYPE_ID='" + cTypeID + "') "
                    + " AND EXISTS(SELECT 1 FROM S_ROLE_GRANT X WHERE (X.ROLE_ID='" + cRoleID + "') AND T.ID=X.NODE_ID) ";
            return DbManager.QueryData(sql);
        }

        public DataRow[] QueryMenuItem(string cTypeID)
        {
            string sql = "SELECT * FROM S_NODES WHERE  (ISACTIVE=1)";
            dtRows = DbManager.QueryData(sql);
            return dtRows.Select(" TYPE_ID='" + cTypeID + "'");
        }
    }
}