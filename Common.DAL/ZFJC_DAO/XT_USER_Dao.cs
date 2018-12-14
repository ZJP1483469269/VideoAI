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
    public class XT_USER_Dao : BaseDao<XT_USER>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(XT_USER vo, String cKeyID)
        {
            ActiveResult vret = new ActiveResult();
            List<String> sqls = new List<string>();
            string sql = null;
            if (cKeyID.Length == 0)
            {
                sql = Insert(vo);
            }
            else
            {
                sql = Update(vo, "user_id='" + cKeyID + "'");
            }
            sqls.Add(sql);
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;

        }
        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "XT_USER", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        public int del_item(String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "";
            sql = "DELETE FROM XT_USER WHERE user_id='" + cDBKey + "'";
            int iCode= DbManager.ExecSQL(sql);
            return iCode;
        }
        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM XT_USER ");
            sql.Append(" WHERE user_id IN (");
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

        public XT_USER FindOne(string cDBKey)
        {
            XT_USER vPhone = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_USER WHERE user_id='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vPhone = new XT_USER();
                ReadDB(vPhone, dtRows);
            }
            return vPhone;
        }
    }
}
