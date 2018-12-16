using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WebSys;
using TLKJ.Utils;
using System.Data;
using TLKJ.DB;

public partial class sysweb_online_list : PageEx
{
    public DataTable dtRows = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        if (!Page.IsPostBack)
        {
            LoginUserInfo vUserInf = getLoginUserInfo(); 
        }
    }
}