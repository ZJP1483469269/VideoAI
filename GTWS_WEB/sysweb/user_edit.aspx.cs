﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;

public partial class sysweb_user_edit : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
    }
}