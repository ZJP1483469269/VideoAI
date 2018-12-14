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
    public class ReceiveSMSService : IService
    {
        XT_RECEIVE_SMS_Dao dao = new XT_RECEIVE_SMS_Dao();
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
            String cWhereParm = "ORG_ID like '" + cORG_ID + "%'";
            String cOrderBy = "ORDER BY ORG_ID ASC";
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
            String cKEY_ID = StringEx.getString(request["RECEIVE_GUID"]);
            if (String.IsNullOrEmpty(cKEY_ID))
            {
                vret = ActiveResult.Valid("错误，RECEIVE_GUID参数为空！");
            }
            else
            {
                XT_RECEIVE_SMS vInfo = dao.FindOne(cKEY_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void check()
        {
            ActiveResult vret = new ActiveResult();
            String cKEY_ID = StringEx.getString(request["RECEIVE_GUID"]);
            int iTYPE_ID = StringEx.getInt(request["TYPE_ID"]);
            int iCode = dao.Check(cKEY_ID, iTYPE_ID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("错误，ORG_ID参数为空！！");
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
