using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.DB;
using TLKJ.WebSys;

public partial class notice : PageEx
{
    public String LAST_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        LAST_ID = DbManager.GetStrValue("SELECT MAX(NOTICE_ID) FROM XT_NOTICE ");
    }
}