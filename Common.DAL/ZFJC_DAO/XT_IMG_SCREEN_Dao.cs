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
    public class XT_IMG_SCREEN_Dao : BaseDao<XT_IMG_SCREEN>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DBResult Query(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*", "XT_IMG_SCREEN", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }

        public int save(XT_IMG_SCREEN vo, String cKeyID)
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
        public XT_IMG_SCREEN FindOne(string cDBKey)
        {
            XT_IMG_SCREEN vo = null;
            DataTable dtRows = DbManager.QueryData("SELECT * FROM XT_IMG_SCREEN WHERE ID='" + cDBKey + "'");
            if ((dtRows != null) && (dtRows.Rows.Count > 0))
            {
                vo = new XT_IMG_SCREEN();
                ReadDB(vo, dtRows);
            }
            return vo;
        }
       

    }
}
