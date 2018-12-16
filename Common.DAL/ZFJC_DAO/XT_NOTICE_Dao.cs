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
    public class XT_NOTICE_Dao : BaseDao<XT_NOTICE>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(XT_NOTICE vo, string cKeyID)
        {
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "notice_id='" + cKeyID + "'");
            }
            sqls.Add(sql);
            return DbManager.ExecSQL(sqls);
        }

        public int del_item(String cDBKey)
        {
            string sql = "";
            sql = "DELETE FROM xt_notice WHERE notice_id='" + cDBKey + "'";
            return DbManager.ExecSQL(sql);
        }

        public DataTable list()
        {
            DBResult ret = DbManager.Query("notice_id,notice_title", "xt_notice", "", "notice_id desc ", 1, 20);
            return ret.dtrows;
        }

        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM xt_notice ");
            sql.Append(" WHERE notice_id IN (");
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
            return DbManager.Query("*", "xt_notice", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public XT_NOTICE FindOne(string cDBKey)
        {
            XT_NOTICE vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM xt_notice WHERE notice_id='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_NOTICE();
                ReadDB(vo, dtRows);
            }
            return vo;
        }

        public XT_NOTICE FindLast()
        {
            XT_NOTICE vo = null;
            DBResult ret = DbManager.Query("*", "xt_notice", "", "order by notice_id desc ", 1, 1);
            DataTable dtRows = ret.dtrows;
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_NOTICE();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
    }
}
