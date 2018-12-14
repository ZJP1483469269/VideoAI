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

        public DataTable QueryList(String cORG_ID, String cTypeID)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "SELECT * FROM S_NODES A,XT_ORG_NODES B "
                    + " WHERE (A.ID=B.NODE_ID) "
                    + " AND (ORG_ID='" + cORG_ID + "') "
                    + " AND (A.ISACTIVE=1) "
                    + " AND (TYPE_ID='" + cTypeID + "')";
            return DbManager.QueryData(sql);
        }

        public DataTable QueryList(String cTypeID)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "SELECT * FROM S_NODES WHERE (TYPE_ID='" + cTypeID + "') AND (ISACTIVE=1)";
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