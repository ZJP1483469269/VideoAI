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
    public class CameraConfigService : IService
    {
        S_PAGE_DICT_Dao dao = new S_PAGE_DICT_Dao();
        XT_CAMERA_Dao CAMERADAO = new XT_CAMERA_Dao();
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
            String cOrderBy = "ORDER BY dict_id ASC";
            DBResult dbret = dao.Query(cWhereParm, cOrderBy, iPageNo, iPageSize);
            DataTable dtRows = dbret.dtrows;
            int iRowsCount = dbret.ROW_COUNT;
            vret = ActiveResult.Query(dtRows);
            vret.total = iRowsCount;

            response.Write(vret.toJSONString());
        }


        public void find()
        {
            XT_CAMERA_CONFIG_Dao camera_config_dao = new XT_CAMERA_CONFIG_Dao();
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            //String cORG_ID = StringEx.getString(request["DICT_ID"]);
            String cID = StringEx.getString(request["ID"]);



            XT_CAMERA xt_camera = CAMERADAO.FindOne(cID);
            String deviceid = xt_camera.device_id;
            XT_CAMERA_CONFIG camera_config = camera_config_dao.FindOne(deviceid);
            String cDevice_ID = camera_config.device_id;

            if (String.IsNullOrEmpty(cDevice_ID)) {
                DbManager.ExecSQL(" INSERT INTO XT_CAMERA_CONFIG SET DICT_ID = '" + deviceid + "'");
            } else {
                XT_CAMERA_CONFIG vInfo = camera_config_dao.FindOne(cDevice_ID);
                vret = ActiveResult.returnObject(vInfo);
            }

            response.Write(vret.toJSONString());
        }

        public void del_item()
        {
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cORG_ID = StringEx.getString(request["DICT_ID"]);    



            if (String.IsNullOrEmpty(cORG_ID))
            {
                vret = ActiveResult.Valid("字典编码不能为空！");
            }
            else
            {
                int iCode = dao.del_item(cORG_ID);
                vret = ActiveResult.Valid(iCode > 0);
            }
            response.Write(vret.toJSONString());
        }

        public void save(){
            ActiveResult vret = ActiveResult.Valid(AppConfig.FAILURE);
            String cID = StringEx.getString(request["DEVICE_ID"]);
            String cOFFSET_P = StringEx.getString(request["OFFSET_P"]);
            String cOFFSET_T = StringEx.getString(request["OFFSET_T"]);
            String cOFFSET_X = StringEx.getString(request["OFFSET_X"]);
            String cOFFSET_H = StringEx.getString(request["OFFSET_H"]);
            String cX = StringEx.getString(request["X"]);
            String cY = StringEx.getString(request["Y"]);

            int iCode = DbManager.ExecSQL(" UPDATE XT_CAMERA_CONFIG SET OFFSET_P = '" + cOFFSET_P + "',OFFSET_T = '" + cOFFSET_T + "',OFFSET_X='" + cOFFSET_X + "',OFFSET_H='" + cOFFSET_H + "',X='" + cX + "',Y='" + cY + "'");
            vret = ActiveResult.Valid(iCode > 0);
            response.Write(vret.toJSONString());

        }
    }
}
