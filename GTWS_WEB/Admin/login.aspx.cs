using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.Utils;
using TLKJ.WebSys;

public partial class Admin_Login : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        log4net.WriteLogFile("Admin_Login");
        if (Config.GetAppSettings(AppConfig.IS_DEBUG).Equals("1"))
        {
            this.user_id.Text = Config.GetAppSettings("DEGUB_USER_NAME", "");
            this.user_pass.Text = Config.GetAppSettings("DEGUB_USER_PASS", "");
        }
    }
}