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
    public class WXUSERService : IService
    {
        WX_USER_Dao dao = new WX_USER_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cAPPID = StringEx.getString(request["APPID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (APPID= '" + cAPPID + "') ";
            String cOrderBy = "ORDER BY NICKNAME ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }

    }
}