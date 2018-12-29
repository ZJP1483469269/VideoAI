using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.Utils;
using TLKJ.WEB;

public partial class Admin_MQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ActiveMQ_Producer vMQ = ActiveMQ_Producer.getInstance();

        String cUserID = AppManager.getIPAddr();

        ActiveMQ_Message vMessage = new ActiveMQ_Message();
        vMessage.FROM_ID = cUserID + "B";
        vMessage.USER_ID = cUserID + "C";
        vMessage.MESSAGE = "中国您好！";
        vMessage.CMD_ID = 11;
        vMQ.SendMSG(vMessage.FROM_ID, vMessage.USER_ID, vMessage);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ActiveMQ_Producer vMQ = ActiveMQ_Producer.getInstance();

        String cUserID = AppManager.getIPAddr();

        ActiveMQ_Message vMessage = new ActiveMQ_Message();
        vMessage.FROM_ID = cUserID + "B";
        vMessage.USER_ID = cUserID + "C";
        vMessage.MESSAGE = "中国您好！";
        vMessage.CMD_ID = 12;
        vMQ.SendMSG(vMessage.FROM_ID, vMessage.USER_ID, vMessage);
    }
}