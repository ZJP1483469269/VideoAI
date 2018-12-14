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
    public class XT_MOBILE_USER_Dao : BaseDao<XT_MOBILE_USER>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(XT_MOBILE_USER vo, String cKeyID)
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
                sql = Update(vo, "USER_COUNT='" + cKeyID + "'");
            }
            sqls.Add(sql);
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;

        }
        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "XT_MOBILE_USER", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public int del_item(String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "";
            sql = "DELETE FROM XT_MOBILE_USER WHERE USER_COUNT='" + cDBKey + "'";
            int iCode= DbManager.ExecSQL(sql);
            return iCode;
        }
        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM XT_MOBILE_USER ");
            sql.Append(" WHERE USER_COUNT IN (");
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

        public XT_MOBILE_USER FindOne(string cDBKey)
        {
            XT_MOBILE_USER vPhone = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_MOBILE_USER WHERE USER_COUNT='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vPhone = new XT_MOBILE_USER();
                ReadDB(vPhone, dtRows);
            }
            return vPhone;
        }
    }
}
