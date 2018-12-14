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
    public class WebJBDao : BaseDao<webjb>
    {
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public DBResult Quertys(String cWhereParm, String cOrderBy, int iPageNo, int iPageSize)
        {
            return DbManager.Query("*,(SELECT ORG_NAME FROM XT_ORG WHERE ORG_ID=A.XIANQU) AS XIANQU_NAME", "XT_JB A", cWhereParm, cOrderBy, iPageNo, iPageSize);
        }
    }
}
