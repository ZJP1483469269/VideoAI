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
    public class XT_JB_Dao : BaseDao<XT_JB>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "XT_JB", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public DBResult QueryResult(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("* "
                +" ,(SELECT RESULT_DATE FROM XT_JB_RESULT X WHERE X.ID=XT_JB.ID) AS RESULT_DATE"
                + ",(SELECT USER_COUNT FROM XT_JB_SEND X WHERE X.ID=XT_JB.ID) AS USER_COUNT"
                + ",(SELECT CREATEDATETIME FROM XT_JB_SEND X WHERE X.ID=XT_JB.ID) AS CREATEDATETIME"                
                + ",(SELECT RESULT FROM XT_JB_RESULT X WHERE X.ID=XT_JB.ID) AS RESULT"
                , "XT_JB", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
        public int save(XT_JB vo, String cKeyID)
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
                sql = Update(vo, "id='" + cKeyID + "'");
            }
            sqls.Add(sql);
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;
        }
        public XT_JB FindOne(string cDBKey)
        {
            XT_JB vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_JB WHERE ID='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_JB();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
        public int Check(string cKEY_ID, int iTYPE_ID)
        {
            ActiveResult vret = new ActiveResult();
            List<string> sqls = new List<string>();
            sqls.Add(" UPDATE XT_JB SET SD_RESULT='" + iTYPE_ID + "' WHERE ID='" + cKEY_ID + "'");
          
            int iCode= DbManager.ExecSQL(sqls);
            return iCode;
        }
     
    }
}
