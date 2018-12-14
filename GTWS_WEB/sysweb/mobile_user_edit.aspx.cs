using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;
using TLKJ.Utils;
using System.Data;
using TLKJ.DB;

public partial class mobile_user_edit : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        LoginUserInfo vUserInf = getLoginUserInfo();
        AREA_ID.Value = AppManager.getAreaHeader(vUserInf.ORG_ID);
        DataTable dtRows = DbManager.QueryData("SELECT ORG_ID,ORG_NAME FROM XT_ORG "
            + " WHERE (ORG_ID<>'" + vUserInf.ORG_ID + "') AND (ORG_ID LIKE '" + vUserInf.ORG_ID + "%') ");
        AppManager.FillDropList(org_id, dtRows, "ORG_ID", "ORG_NAME");
    }
}