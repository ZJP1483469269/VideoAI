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
    public class CameraService : IService
    {
        XT_CAMERA_Dao dao = new XT_CAMERA_Dao();
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }

        public void query()
        {
            ActiveResult vret = new ActiveResult();
            String cORG_ID = StringEx.getString(request["ORG_ID"]);

            String cWhereParm = "";
            String cOrderBy = "ORDER BY ID ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;
            response.Write(vret.toJSONString());
        }
        public void Child()
        {
            String cKeyID = StringEx.getString(request["ID"]);
            String cVKey, cVName;
            List<Dictionary<string, string>> arKeys = new List<Dictionary<string, string>>();
            int iTypeID = StringEx.getInt(cKeyID);
            DataTable dtRows = null;
            if (cKeyID.Length == 6)
            {
                dtRows = dao.QueryData("DISTINCT VILLAGE_ID AS ID, VILLAGE AS TEXT", " (ORG_ID='" + cKeyID + "') ", " order by VILLAGE ");
            }
            else
            {
                dtRows = dao.QueryData("ID, DEVICE_ID, ADDR AS TEXT", " (VILLAGE_ID='" + cKeyID + "') ", " order by ADDR ");
            }

            for (int i = 0; i < dtRows.Rows.Count; i++)
            {
                cVKey = StringEx.getString(dtRows, i, "ID");
                cVName = StringEx.getString(dtRows, i, "TEXT");
                Dictionary<string, string> vo = new Dictionary<string, string>();
                vo.Add("id", cVKey);
                vo.Add("text", cVName);
                vo.Add("name", cVName);
                vo.Add("pid", cKeyID);
                vo.Add("value", cVKey);
                if (cKeyID.Length == 6)
                {
                    vo.Add("isParent", "true");
                    vo.Add("device_id", "");
                }
                else
                {
                    vo.Add("isParent", "false");
                    vo.Add("device_id", StringEx.getString(dtRows, i, "DEVICE_ID"));
                }
                arKeys.Add(vo);
            }

            // response.Write(cStart + JsonLib.toJSONString(arKeys) + cFinish);
            response.Write(JsonLib.ToJSON(arKeys));
        }
        public void find()
        {
            ActiveResult vret = new ActiveResult();
            // String cKeyID = StringEx.getString(request["ID"]);
            String cKeyID = StringEx.getString(request["ID"]);
            XT_CAMERA vo = dao.FindOne(cKeyID);
            vret = ActiveResult.returnObject(vo);
            response.Write(vret.toJSONString());
        }

        public void updatefromvideo()
        {
            ActiveResult vret = new ActiveResult();
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            String cIDS = StringEx.getString(request["IDS"]);
            String cNAMES = StringEx.getString(request["NAMES"]);
            int iCode = dao.UpdateFromVideo(cORG_ID, cIDS, cNAMES);
            vret = ActiveResult.Valid(iCode);
            response.Write(vret.toJSONString());
        }

        public void county_list()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cAreaID = cORG_ID.Replace("00", "");
            String cWhereParm = "ORG_ID like '" + cAreaID + "%'";
            String cOrderBy = "ORDER BY ORG_ID ASC";
            DataTable dtRows = dao.QueryData("distinct ORG_ID,COUNTY AS ORG_NAME", cWhereParm, cOrderBy);
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }

        public void village_list()
        {
            String cORG_ID = StringEx.getString(request["ORG_ID"]);
            String cAreaID = cORG_ID.Replace("00", "");
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cWhereParm = " (ORG_ID like '" + cAreaID + "%') ";
            String cOrderBy = "ORDER BY ORG_ID ASC";
            DataTable dtRows = dao.QueryData("distinct VILLAGE AS ORG_ID,VILLAGE AS ORG_NAME", cWhereParm, cOrderBy);
            vret = ActiveResult.Query(dtRows);
            response.Write(vret.toJSONString());
        }

    }
}
