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
    public class NoticeService : IService
    {
        XT_NOTICE_Dao dao = new XT_NOTICE_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            DBResult dbret = dao.Query(null, "ORDER BY NOTICE_ID DESC", iPageNo, iPageSize);
            vret = ActiveResult.Query(dbret.dtrows);
            vret.total = dbret.ROW_COUNT;
            response.Write(vret.toJSONString());
        }

        public void save()
        {
            ActiveResult vret = new ActiveResult();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            String cORG_ID = StringEx.getString(request[AppConfig.ORG_ID]);
            XT_NOTICE vo = new XT_NOTICE();
            vo = (XT_NOTICE)RequestUtil.readFromRequest(request, vo);
            vo.notice_date = vo.notice_date.Replace("-", "");
            vo.notice_id = AutoID.getAutoID(cORG_ID);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void last()
        {
            ActiveResult vret = new ActiveResult();
            XT_NOTICE vo = dao.FindLast();
            vret = ActiveResult.returnObject(vo);
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request[DEL_ITEM_KEY]);
            int iCode = dao.del_item(cDBKey);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void del_list()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request[DEL_LIST_KEY]);
            int iCode = dao.del_list(cDBKey);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void finditem(String cDBKey)
        {
            ActiveResult vret = new ActiveResult();
            XT_NOTICE vo = dao.FindOne(cDBKey);
            vret = ActiveResult.returnObject(vo);
            response.Write(vret.toJSONString());
        }
    }
}
