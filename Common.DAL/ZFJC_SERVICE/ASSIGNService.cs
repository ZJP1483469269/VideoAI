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
    public class ASSIGNService : IService
    {
        XT_JB_SEND_Dao dao = new XT_JB_SEND_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void find()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cKeyID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKeyID))
            {
                vret = ActiveResult.Valid("ID不能为空！");
            }
            else
            {
                XT_JB_SEND vInfo = dao.FindOne(cKeyID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cKeyID = StringEx.getString(request["id"]);
            if (String.IsNullOrEmpty(cKeyID))
            {
                vret = ActiveResult.Valid("ID不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cKeyID);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.SUCCESS);
            String cKeyID = StringEx.getString(request["ID"]);
            String cORG_ID = StringEx.getString(request["org_id"]);
            String cAREA_ID = StringEx.getString(request["AREA_ID"]);
            cAREA_ID = cAREA_ID.Replace("00", "");

            XT_JB_SEND vo = new XT_JB_SEND();
            vo.createdatetime = DateTime.Now.ToString("yyyy-MM-dd");
            vo = (XT_JB_SEND)RequestUtil.readFromRequest(request, vo);

            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码不能为空！");
            }
            else if (String.IsNullOrEmpty(vo.user_count))
            {
                vret = ActiveResult.Valid("用户账号不能为空！");
            }

            if (vret.result == AppConfig.SUCCESS)
            {
                int iCode = dao.save(vo,"");
                vret = ActiveResult.Valid(iCode);
            }
            response.Write(vret.toJSONString());
        }
    }
}