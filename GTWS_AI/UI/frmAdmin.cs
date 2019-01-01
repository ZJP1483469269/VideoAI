using GTWS_TASK.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.SYS;
using TLKJ.Utils;

namespace GTWS_AI.UI
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            frmMain vDialog = new frmMain(); 
            vDialog.Hide();
        }
    }
}
