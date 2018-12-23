using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using TLKJ.DB;
using System.Data;
using TLKJ.Utils;

public partial class alarm_ImageList : PageEx
{
    public DataTable dtRows;
    protected void Page_Load(object sender, EventArgs e)
    {
        String sql = "";
        String cDeviceID = StringEx.getString(Request.QueryString["DEVICE_ID"]);
        if (!String.IsNullOrEmpty(cDeviceID))
        {
            sql = "SELECT TOP 5 * FROM XT_IMG_REC A,XT_CAMERA b WHERE a.camera_id=b.device_id AND B.device_id like '" + cDeviceID + "%'";
        }
        else
        {
            sql = "SELECT TOP 20 * FROM XT_IMG_REC A,XT_CAMERA b WHERE a.camera_id=b.device_id  ";
        }
        sql = sql + " ORDER BY REC_ID DESC ";
        DataTable dtRows = DbManager.QueryData(sql.ToString());
    }
}