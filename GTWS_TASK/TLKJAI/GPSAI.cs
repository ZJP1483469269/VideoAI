using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using TLKJ.Utils;
using OpenCvSharp;
using System.IO;
using OpenCvSharp.Extensions;
using Tesseract;
using System.Data;
using TLKJ.DB;

namespace TLKJAI
{
    public class GPSAI
    {
        Dictionary<String, GPSXY> CameraList = new Dictionary<String, GPSXY>();
        public GPSXY getCenter(String cCameraID, float P, float T, float X)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM XT_CAMERA_CONFIG WHERE DEVICE_ID='" + cCameraID + "'");
            DataTable dtRows = DbManager.QueryData(sql.ToString());
            if (dtRows != null && dtRows.Rows.Count > 0)
            {
                GPSXY vGPS = new GPSXY();
                //113.807187 34.170334
                vGPS.X = StringEx.GetDouble(dtRows, 0, "X");
                vGPS.Y = StringEx.GetDouble(dtRows, 0, "Y");
                return vGPS;
            }
            else
            {
                return null;
            }
        }


    }
}

