using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using TLKJ.Utils;

public partial class mobile_user_list : PageEx
{
    public String ORG_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        LoginUserInfo vUserInf = getLoginUserInfo();
        ORG_ID = vUserInf.ORG_ID;
    }
}