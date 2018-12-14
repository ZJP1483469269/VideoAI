using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TLKJ.Utils;
using TLKJ.DB;
using System.Data;
using System.IO;
using System.Web.UI;

namespace TLKJ.DAO
{
    public class XT_IMG_SCREENService : IService
    {
        XT_IMG_SCREEN_Dao dao = new XT_IMG_SCREEN_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }
        public void saves()
        {
            ActiveResult vret = new ActiveResult();
            XT_IMG_SCREEN vo = new XT_IMG_SCREEN();
            String cKeyID = StringEx.getString(request[AppConfig.__DBKEY]);
            vo = (XT_IMG_SCREEN)RequestUtil.readFromRequest(request, vo);
            int iCode = dao.save(vo, cKeyID);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            String cWhereParm = " ";
            String cOrderBy = " order by id asc";
            DBResult dbret = DbManager.Query("*", "XT_IMG_SCREEN", cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            vret = ActiveResult.Query(dtRows);
            vret.total = dbret.ROW_COUNT;
            response.Write(vret.toJSONString());
        }

        public void find()
        {
            ActiveResult vret = new ActiveResult();
            String cDBKey = StringEx.getString(request["ID"]);
            DataTable dtRows = DbManager.QueryData("select  * from XT_IMG_SCREEN where id='" + cDBKey + "'");
            String cCUTOUT_TXT = StringEx.getString(dtRows, 0, "CUTOUT_TXT");
            int i = cCUTOUT_TXT.IndexOf(",");
            dtRows.Columns.Add("CHINESE");
            StringBuilder cChinese = new StringBuilder();
            if (i > 0)
            {

                string[] sArray = cCUTOUT_TXT.Split(',');
                foreach (string cEN in sArray)
                {
                    String cCH = DbManager.GetStrValue("select CHINESE from XT_IMG_DICT where ENGLISH  like  '%" + cEN + "%'");
                    if (String.IsNullOrEmpty(cCH))
                    {
                        cChinese.Append(cEN + ",");
                    }
                    else
                    {
                        cChinese.Append(cCH + ",");
                    }

                }
            }
           
            dtRows.Rows[0]["CHINESE"] = cChinese.ToString();
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }


    }
}
