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
        public void GetStrValue()
        {
            String sql = StringEx.getString(request["SQL"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cStr = DbManager.GetStrValue(sql);
            vret = ActiveResult.returnObject(cStr);
            response.Write(vret.toJSONString());
        }

        public void QueryData()
        {
            String sql = StringEx.getString(request["SQL"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            DataTable dtRows = DbManager.QueryData(sql);
            String cStr = JsonLib.ToJSON(dtRows);
            vret = ActiveResult.returnObject(cStr);
            response.Write(vret.toJSONString());
        }
        public void Query()
        {
            String cFileList = StringEx.getString(request["FILELIST"]);
            String cWhereParm = StringEx.getString(request["WHEREPARM"]);
            String cTableName = StringEx.getString(request["TABLENAME"]);
            String cOrderBy = StringEx.getString(request["CORDERBY"]);

            String cPAGENO = StringEx.getString(request["PAGENO"]);
            String cPAGESIZE = StringEx.getString(request["PAGESIZE"]);

            int iPageNo = 1;
            int iPageSize = 15;

            if (!String.IsNullOrWhiteSpace(cPAGENO))
            {
                iPageNo = StringEx.getInt(cPAGENO);
            }

            if (!String.IsNullOrWhiteSpace(cPAGESIZE))
            {
                iPageSize = StringEx.getInt(cPAGESIZE);
            }
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            DBResult rs = DbManager.Query(cFileList, cTableName, cWhereParm, cOrderBy, iPageNo, iPageSize);
            vret = ActiveResult.returnObject(rs.dtrows);
            vret.total = rs.ROW_COUNT;
            response.Write(vret.toJSONString());
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        public void ExecSQL()
        {
            List<String> sqls = new List<string>();
            List<Object[]> objs = new List<Object[]>();
            for (int i = 0; i < 1000; i++)
            {
                String cSQL = StringEx.getString(request["SQL_" + i]);
                String cPARM = StringEx.getString(request["PARM_" + i]);
                if (cSQL.Length > 0)
                {
                    sqls.Add(cSQL);
                    if (cPARM.Length > 0)
                    {
                        Object[] parmList = JsonLib.ToObject<Object[]>(cPARM);
                        objs.Add(parmList);
                    }
                    else
                    {
                        objs.Add(null);
                    }
                }
                else
                {
                    break;
                }
            }
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            int iCode = DbManager.ExecSQL(sqls, objs);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }
    }
}
