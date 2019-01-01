using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.Utils;
using TLKJ.WEB;

public partial class Admin_Login : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.WriteLogFile("Admin_Login");
        if (!Page.IsPostBack)
        {
            if (Config.GetAppSettings(AppConfig.IS_DEBUG).Equals("1"))
            {
                this.user_id.Text = Config.GetAppSettings("USER_NAME", "");
                this.user_pass.Text = Config.GetAppSettings("USER_PASS", "");
            }
        }
    }
}