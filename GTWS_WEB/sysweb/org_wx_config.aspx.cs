using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using System.Text;
using System.Data;
using TLKJ.DB;
using TLKJ.Utils;

public partial class sysweb_org_wx_config : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
    }
}