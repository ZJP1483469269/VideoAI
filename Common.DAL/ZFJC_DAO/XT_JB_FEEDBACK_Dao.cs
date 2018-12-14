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
    public class XT_JB_FEEDBACK_Dao : BaseDao<XT_JB_FEEDBACK>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DataTable Query(String cWhereParm, String cOrderBy)
        {
            return DbManager.QueryData("*", "XT_JB_FEEDBACK", cWhereParm);
        }

        public int save(XT_JB_FEEDBACK vo, String cKeyID)
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

        public XT_JB_FEEDBACK FindOne(string cDBKey)
        {
            XT_JB_FEEDBACK vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_JB_FEEDBACK WHERE ID='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_JB_FEEDBACK();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
    }
}
