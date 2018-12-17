using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;
using System.Data;
using TLKJ.DB;

public partial class alarm_ImageOnline : PageEx
{
    public DataTable dtRows = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        String sql = "";
        sql = "SELECT TOP 20 * FROM XT_IMG_REC A,XT_CAMERA b WHERE a.camera_id=b.device_id  ";

        sql = sql + " ORDER BY REC_ID DESC ";
        dtRows = DbManager.QueryData(sql.ToString());
    }
}