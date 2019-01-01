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
    public class TemplateService : IService
    {
        S_TEMPLATE_Dao dao = new S_TEMPLATE_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = "ORG_ID ='" + cORG_ID + "'";
            String cOrderBy = "ORDER BY TEMPLATE_ID ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }


        public void find()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["TEMPLATE_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("用户编码参数不能为空！");
            }
            else
            {
                S_TEMPLATE vInfo = dao.FindOne(cORG_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["TEMPLATE_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("用户编码参数不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cORG_ID);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }
    }
}
