using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.Utils;

public partial class api_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.WriteTextLog("USER_ID:" + StringEx.getString(Request["USER_ID"]));
        log4net.WriteTextLog("USER_NAME:" + StringEx.getString(Request["USER_NAME"]));
        log4net.WriteTextLog("Files:" + this.Request.Files.Count.ToString());
    }
}