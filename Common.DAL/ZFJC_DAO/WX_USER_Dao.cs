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
    public class WX_USER_Dao : BaseDao<WX_USER>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(WX_USER vo, String cAppID, String cKeyID)
        {
            ActiveResult vret = new ActiveResult();
            List<String> sqls = new List<string>();
            string sql = null;

            sql = Update(vo, " APPID='" + cAppID + "' AND OPENID='" + cKeyID + "'");
            int iCode = DbManager.ExecSQL(sql);
            if (iCode < 1)
            {
                sql = Insert(vo);
                iCode = DbManager.ExecSQL(sql);
                return iCode;
            }
            else
            {
                return iCode;
            }
        }
        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "WX_USER", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public int del_item(String cAppID, String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "";
            sql = "DELETE FROM WX_USER WHERE  OPENID='" + cDBKey + "'";
            int iCode= DbManager.ExecSQL(sql); 
            return iCode;
        }

        public int del_list(String cAppID, String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM WX_USER ");
            sql.Append(" WHERE OPENID IN (");
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

        public WX_USER FindOne(String cAppID, string cOpenID)
        {
            WX_USER vPhone = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM WX_USER WHERE AppID='" + cAppID + "' and OPENID='" + cOpenID + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vPhone = new WX_USER();
                ReadDB(vPhone, dtRows);
            }
            return vPhone;
        }
    }
}
