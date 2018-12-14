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
    public class SQLDBService : IService
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        /// <summary>
        /// 查询数据语句
        /// </summary>
        public void query()
        {
            String sql = StringEx.getString(request["SQL"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            DataTable dtRows = DbManager.QueryData(sql);
            String cStr = Newtonsoft.Json.JsonConvert.SerializeObject(dtRows);
            response.Write(cStr);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        public void execute()
        {
            List<String> sqls = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                String sql = StringEx.getString(request["SQL_" + i]);
                if (sql.Length > 0)
                {
                    sqls.Add(sql);
                }
                else
                {
                    break;
                }
            }
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            int iCode = DbManager.ExecSQL(sqls);
            response.Write(iCode);
        }
    }
}
