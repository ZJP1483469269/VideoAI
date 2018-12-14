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
    public class DBService : IService
    {
        DB_Dao dao = new DB_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            String sql = StringEx.getString(request["SQLTEXT"]);
            DataTable dtRows = dao.query(sql);
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }

        public void execsql()
        {
            ActiveResult vret = new ActiveResult();
            String sql = StringEx.getString(request["SQLTEXT"]);
            int iCode = dao.execsql(sql);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }
    }
}
