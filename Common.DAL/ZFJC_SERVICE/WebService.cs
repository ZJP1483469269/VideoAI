using System;
using System.Web;
using System.Data;
using TLKJ.DAO;
using TLKJ.Utils;
using TLKJ.DB;

namespace TLKJ.DAO
{
    public class WebService : IService
    {
        WebJBDao webDao = new WebJBDao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }
        public void fetch()
        {
            String cOrgID = StringEx.getString(request["ORG_ID"]);
            ActiveResult vret = new ActiveResult();
            String cWhereParm = " (ORG_ID='" + cOrgID + "') AND (JBTYPE='WEB') ";
            String cOrderBy = " order by ID";
            DBResult dbret = webDao.Quertys(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowCount = (dtRows == null) ? 0 : dtRows.Rows.Count;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowCount;
            response.Write(vret.toJSONString());
        }
    }
}
