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
    public class WX_RECV_MSG_Dao : BaseDao<WX_RECV_MSG>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*,(SELECT NICKNAME FROM WX_USER X WHERE X.OPENID=WX_RECV_MSG.OPENID) AS NICKNAME", "WX_RECV_MSG", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        public int del_item(String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "";
            sql = "DELETE FROM WX_RECV_MSG WHERE ID='" + cDBKey + "'";
               int iCode   = DbManager.ExecSQL(sql); 
            return iCode;
        }

        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM WX_RECV_MSG ");
            sql.Append(" WHERE ID IN (");
            for (int i = 0; i < KeyList.Length; i++)
            {
                String cUserID = KeyList[i].Trim();
                if (i == 0)
                {
                    sql.Append("'" + cUserID + "'");
                }
                else
                {
                    sql.Append(",'" + cUserID + "'");
                }
            }
            sql.Append(" )");
            return DbManager.ExecSQL(sql.ToString());
        }

        public WX_RECV_MSG FindOne(string cDBKey)
        {
            WX_RECV_MSG vPhone = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM WX_RECV_MSG WHERE ID='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vPhone = new WX_RECV_MSG();
                ReadDB(vPhone, dtRows);
            }
            return vPhone;
        }

    }
}
