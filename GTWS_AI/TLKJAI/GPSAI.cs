using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using TLKJ.Utils;
using System.IO;
using System.Data;
using TLKJ_IVS; 

namespace TLKJAI
{
    public class GPSAI
    {
        Dictionary<String, GPSXY> CameraList = new Dictionary<String, GPSXY>();
        public GPSXY getCenter(String cCameraID, float P, float T, float X)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM XT_CAMERA_CONFIG WHERE DEVICE_ID='" + cCameraID + "'");
            DataTable dtRows = WebSQL.QueryData(sql.ToString());
            if (dtRows != null && dtRows.Rows.Count > 0)
            {
                GPSXY vGPS = new GPSXY();
                //113.807187 34.170334
                vGPS.X = StringEx.GetDouble(dtRows, 0, "X");
                vGPS.Y = StringEx.GetDouble(dtRows, 0, "Y");

                Double dOffsetX = StringEx.GetDouble(dtRows, 0, "OFFSET_X");
                Double dOffsetY = StringEx.GetDouble(dtRows, 0, "OFFSET_Y");

                Double dStartP = StringEx.GetDouble(dtRows, 0, "START_P");
                Double dStartT = StringEx.GetDouble(dtRows, 0, "START_T");
                Double dStartX = StringEx.GetDouble(dtRows, 0, "START_X");
                Double dStartH = StringEx.GetDouble(dtRows, 0, "START_H");

                return vGPS;
            }
            else
            {
                return null;
            }
        }

        public GPSXY getX(GPSXY vGPS, Double dOffsetX, Double dOffsetY, Double vStartP, Double vP, Double vStartT, Double vT, Double vHeight)
        {
            Double fRealP = (vP - vStartP);
            //Double iOffsetX = (vT / Math.Sin(vStartT)) * Math.Sin(vT);      //南北偏移量
            //Double iOffsetY = iOffsetX * Math.Sin(fRealP);

            Double fY = vHeight / Math.Tan(vT);
            vGPS.Y = vGPS.Y + fY / 0000006;

            Double fX = fY * Math.Tan(fRealP);
            vGPS.X = vGPS.X + fY;
            return vGPS;
        }
    }
}

