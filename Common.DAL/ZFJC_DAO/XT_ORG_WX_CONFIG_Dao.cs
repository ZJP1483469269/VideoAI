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
    public class S_ORG_INF_WX_CONFIG_Dao : BaseDao<S_ORG_INF_WX_CONFIG>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(S_ORG_INF_WX_CONFIG vo, string cKeyID)
        {
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "org_id='" + cKeyID + "'");
            }
            sqls.Add(sql);
            return DbManager.ExecSQL(sqls);
        }

        public int del_item(String cDBKey)
        {
            string sql = "";
            sql = "DELETE FROM S_ORG_INF_WX_CONFIG WHERE org_id='" + cDBKey + "'";
            return DbManager.ExecSQL(sql);
        }

        public DataTable Query(String cWhereParm)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM S_ORG_INF_WX_CONFIG ");
            if (!String.IsNullOrEmpty(cWhereParm))
            {
                sql.Append(" WHERE " + cWhereParm);
            }
            return DbManager.QueryData(sql.ToString());
        }

        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM S_ORG_INF_WX_CONFIG ");
            sql.Append(" WHERE org_id IN (");
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

        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "S_ORG_INF_WX_CONFIG", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public S_ORG_INF_WX_CONFIG FindOne(string cDBKey)
        {
            S_ORG_INF_WX_CONFIG vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM S_ORG_INF_WX_CONFIG WHERE org_id='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new S_ORG_INF_WX_CONFIG();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
    }
}
