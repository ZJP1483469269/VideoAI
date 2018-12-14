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
    public class MOBILE_USERService : IService
    {
        XT_MOBILE_USER_Dao dao = new XT_MOBILE_USER_Dao();
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

            String cOrderBy = "ORDER BY USER_COUNT ASC";
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
            String cKeyID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKeyID))
            {
                vret = ActiveResult.Valid("用户账号不能为空！");
            }
            else
            {
                XT_MOBILE_USER vInfo = dao.FindOne(cKeyID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cUSER_COUNT = StringEx.getString(request["dbkey"]);
            if (String.IsNullOrEmpty(cUSER_COUNT))
            {
                vret = ActiveResult.Valid("用户账号不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cUSER_COUNT);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.SUCCESS);
            String cKeyID = StringEx.getString(request["ID"]);
            String cUser_Count = StringEx.getString(request["user_count"]);
            String cORG_ID = StringEx.getString(request["org_id"]);
            String cAREA_ID = StringEx.getString(request["AREA_ID"]);
            cAREA_ID = cAREA_ID.Replace("00", "");

            XT_MOBILE_USER vo = new XT_MOBILE_USER();
            vo = (XT_MOBILE_USER)RequestUtil.readFromRequest(request, vo);

            vo.user_count = vo.phone_num;
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码不能为空！");
            }
            else if (String.IsNullOrEmpty(vo.phone_num))
            {
                vret = ActiveResult.Valid("手机号码不能为空！");
            }

            if (vret.result == AppConfig.SUCCESS)
            {
                int iCode = dao.save(vo, cKeyID);
                vret = ActiveResult.Valid(iCode);
            }
            response.Write(vret.toJSONString());
        }
    }
}