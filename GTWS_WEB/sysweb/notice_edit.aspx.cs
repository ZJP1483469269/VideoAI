using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;

public partial class notice_edit : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        ORG_ID.Value = getLoginUserInfo().ORG_ID;
        notice_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
}