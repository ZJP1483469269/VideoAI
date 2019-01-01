using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;

public partial class sysweb_template_edit : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        if (!Page.IsPostBack)
        {
            org_id.Value = this.getLoginUserInfo().ORG_ID;
        }
    }
}