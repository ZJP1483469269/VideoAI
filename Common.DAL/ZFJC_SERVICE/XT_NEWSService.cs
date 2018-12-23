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
    public class XT_NEWSService : IService
    {
        XT_NEWS_Dao dao = new XT_NEWS_Dao();
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
            String cKeyID = StringEx.getString(request["ID"]);
            if (String.IsNullOrEmpty(cKeyID))
            {
                vret = ActiveResult.Valid("用户账号不能为空！");
            }
            else
            {
                XT_NEWS vInfo = dao.FindOne(cKeyID);
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
                vret = ActiveResult.Valid("编码不能为空！");
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
            String cORG_ID = StringEx.getString(request["org_id"]);
            String cCLS_ID = StringEx.getString(request["cls_id"]);

            XT_NEWS vo = new XT_NEWS();
            vo = (XT_NEWS)RequestUtil.readFromRequest(request, vo);
            vo.cls_id = cCLS_ID;
            vo.update_time = StringEx.getString(DateUtils.getDayTimeNum());
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码不能为空！");
            }
            else if (String.IsNullOrEmpty(cCLS_ID))
            {
                vret = ActiveResult.Valid("类型不能为空！");
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