using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.DAO;
using System.Data;
using TLKJ.Utils;
using TLKJ.WebSys;

public partial class Admin_left : PageEx
{
    public S_NODES_Dao dao = new S_NODES_Dao();
    public DataTable dtRows;
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
        String cTypeID = StringEx.getString(Request.QueryString["TYPE_ID"]);
        dtRows = dao.QueryList(cTypeID);
    }
}