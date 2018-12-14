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
    public class RECV_MSGService : IService
    {
        WX_RECV_MSG_Dao dao = new WX_RECV_MSG_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            String cWX_ID = StringEx.getString(request["WX_ID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (WX_ID ='" + cWX_ID + "') ";
            String cOrderBy = "ORDER BY ID DESC";
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
            String cKEY_ID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKEY_ID))
            {
                vret = ActiveResult.Valid("错误，ID参数为空！");
            }
            else
            {
                WX_RECV_MSG vInfo = dao.FindOne(cKEY_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cKEY_ID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKEY_ID))
            {
                vret = ActiveResult.Valid("错误，ID参数为空！！");
            }
            else
            {
                int iCode = dao.del_item(cKEY_ID);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }
    }
}
