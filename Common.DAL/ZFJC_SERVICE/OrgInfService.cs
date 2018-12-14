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
    public class OrgInfService : IService
    {
        XT_ORG_Dao dao = new XT_ORG_Dao();
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
            cORG_ID = cORG_ID.Replace("00", "");
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
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码参数不能为空！");
            }
            else
            {
                XT_ORG vInfo = dao.FindOne(cORG_ID);
                vret = ActiveResult.returnObject(vInfo);
            }
            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码参数不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cORG_ID);
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

            XT_ORG vo = new XT_ORG();
            vo = (XT_ORG)RequestUtil.readFromRequest(request, vo);

            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("单位编码不能为空！");
            }
            else if (!cORG_ID.StartsWith(cAREA_ID))
            {
                vret = ActiveResult.Valid("单位编码格式错误！");
            }
            else if (String.IsNullOrEmpty(vo.org_name))
            {
                vret = ActiveResult.Valid("单位简称不能为空！");
            }
            else if (String.IsNullOrEmpty(vo.org_full_name))
            {
                vret = ActiveResult.Valid("单位全称不能为空！");
            }

            String cPloygn = vo.ploygn;
            if (!String.IsNullOrEmpty(cPloygn))
            {
                String cWebRoot = request.MapPath("/");
                String cFileName = cWebRoot + @"static\area_range\" + cORG_ID + ".js";
                FileLib.WriteTextFile(cFileName, cPloygn);
            }
            vo.org_type = cORG_ID.Length;
            if (vret.result == AppConfig.SUCCESS)
            {
                int iCode = dao.save(vo, cKeyID);
                vret = ActiveResult.Valid(iCode);
            }
            response.Write(vret.toJSONString());
        }
    }
}