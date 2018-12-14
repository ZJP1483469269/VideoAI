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
    public class XT_RECEIVE_SMS_Dao : BaseDao<XT_RECEIVE_SMS>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }
        public int save(XT_RECEIVE_SMS vo, String cKeyID)
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
                sql = Update(vo, "RECEIVE_GUID='" + cKeyID + "'");
            }
            sqls.Add(sql);
            int iCode = DbManager.ExecSQL(sqls); 
            return iCode;
        }

        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "XT_RECEIVE_SMS", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        public int del_item(String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            string sql = "";
            sql = "DELETE FROM XT_RECEIVE_SMS WHERE RECEIVE_GUID='" + cDBKey + "'";
            int iCode= DbManager.ExecSQL(sql); 
            return iCode;
        }
        public int del_list(String cKeyList)
        {
            String[] KeyList = cKeyList.Split(',');
            ActiveResult vret = new ActiveResult();
            StringBuilder sql = new StringBuilder();
            sql.Append(" DELETE FROM XT_RECEIVE_SMS ");
            sql.Append(" WHERE RECEIVE_GUID IN (");
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

        public XT_RECEIVE_SMS FindOne(string cDBKey)
        {
            XT_RECEIVE_SMS vPhone = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_RECEIVE_SMS WHERE RECEIVE_GUID='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vPhone = new XT_RECEIVE_SMS();
                ReadDB(vPhone, dtRows);
            }
            return vPhone;
        }

        public int Check(string cKEY_ID, int iSD_RESULT)
        {
            ActiveResult vret = new ActiveResult();
            List<string> sqls = new List<string>();
            sqls.Add(" UPDATE XT_RECEIVE_SMS SET SD_RESULT='" + iSD_RESULT + "',READ_FLAG=1 WHERE RECEIVE_GUID='" + cKEY_ID + "'");
            if (iSD_RESULT == 1)
            {
                sqls.Add(" INSERT INTO XT_JB(ID,ORG_ID,SD_RESULT,PHONE,NEIRONG,JBTYPE,TIME) "
                    + " SELECT RECEIVE_GUID,ORG_ID,SD_RESULT,RECEIVE_TELNO,RECEIVE_MESSAGE,'SMS',RECEIVE_TIME "
                    + " FROM XT_RECEIVE_SMS "
                    + " WHERE RECEIVE_GUID='" + cKEY_ID + "'");
            }
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;
        }
    }
}
