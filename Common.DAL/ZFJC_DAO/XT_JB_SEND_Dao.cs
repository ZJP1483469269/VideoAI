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
    public class XT_JB_SEND_Dao : BaseDao<XT_JB_SEND>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(XT_JB_SEND vo, string cKeyID)
        {
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "id='" + cKeyID + "'");
            }
            sqls.Add(sql);
            return DbManager.ExecSQL(sqls);
        }

        public int del_item(String cDBKey)
        {
            string sql = "";
            sql = "DELETE FROM XT_JB_SEND WHERE id='" + cDBKey + "'";
            return DbManager.ExecSQL(sql);
        }

        public DataTable list()
        {
            DBResult ret = DbManager.Query(" ID,ORG_ID,USER_COUNT,CREATEDATETIME,REMARK", "XT_JB_SEND", "", "id desc ", 1, 20);
            return ret.dtrows;
        }

        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM XT_JB_SEND ");
            sql.Append(" WHERE id IN (");
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
            return DbManager.Query("*", "XT_JB_SEND", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public XT_JB_SEND FindOne(string cDBKey)
        {
            XT_JB_SEND vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_JB_SEND WHERE id='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_JB_SEND();
                ReadDB(vo, dtRows);
            }
            return vo;
        }

        public XT_JB_SEND FindLast()
        {
            XT_JB_SEND vo = null;
            DBResult ret = DbManager.Query("*", "XT_JB_SEND", "", "id desc ", 1, 1);
            DataTable dtRows = ret.dtrows;
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_JB_SEND();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
    }
}
