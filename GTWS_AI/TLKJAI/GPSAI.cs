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
        public GPSXY getCenter(String cCameraID, float fP, float fT, float fX)
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

                Double dOFFSET_P = StringEx.GetDouble(dtRows, 0, "OFFSET_P");
                Double dOFFSET_T = StringEx.GetDouble(dtRows, 0, "OFFSET_T");
                Double dOFFSET_X = StringEx.GetDouble(dtRows, 0, "OFFSET_X");
                Double dOFFSET_H = StringEx.GetDouble(dtRows, 0, "OFFSET_H");

                Double fGPS_Y = getY(vGPS.Y, dOFFSET_H, fT);
                return vGPS;
            }
            else
            {
                return null;
            }
        }

        public GPSXY getCenter(GPSXY vGPS, float vP, float vT, float vOffsetH, float vOffsetP, float vOffsetT)
        {
            GPSXY vInfo = new GPSXY();
            //摄像机角度转长度，单位（米）
            Double dWidth = vOffsetH * Math.Tan(Math.PI / (180 / vOffsetT));

            Double dVal = (vP - vOffsetP);
            if (dVal < 0)
            {
                dVal = dVal + 360;
            }
            Double dOffsetX = dWidth * Math.Cos(Math.PI / (180 / dVal));
            Double dOffsetY = dWidth * Math.Sin(Math.PI / (180 / dVal));
            vInfo.X = vInfo.X + getDegree(dOffsetX);
            vInfo.Y = vInfo.Y + getDegree(dOffsetY);
            return vInfo;
        }

        public double getDegree(double fVal)
        {
            return fVal / (2 * Math.PI * 6371004) * 360;
        }
        public double getMeter(double fVal)
        {
            return fVal / 360 * 2 * (2 * Math.PI * 6371004);
        }
        public Double getY(Double fGpsX, Double iCameaHeight, Double T)
        {
            //距离,单位米
            Double fY = iCameaHeight * Math.Tan(Math.PI / (180 / T));
            return fGpsX + fY * (1 / 60000);
        }

        public Double getX(Double fGpsX, Double iCameaHeight, Double T)
        {
            //距离,单位米
            Double fY = iCameaHeight * Math.Tan(Math.PI / (180 / T));
            return fGpsX + fY * (1 / 60000);
        }
    }

}

