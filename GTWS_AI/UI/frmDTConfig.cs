using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;

namespace GTWS_TASK.UI
{
    public partial class frmDTConfig : frmBase
    {
        public frmDTConfig()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            INIConfig.Write("TASK", AppConfig.DAY_AM, this.txtDAY_AM.Text);
            INIConfig.Write("TASK", AppConfig.DAY_PM, this.txtDAY_PM.Text);
            this.Close();
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.txtDAY_AM.Text = INIConfig.ReadString("TASK", AppConfig.DAY_AM);
            this.txtDAY_PM.Text = INIConfig.ReadString("TASK", AppConfig.DAY_PM);
        }
    }
}
