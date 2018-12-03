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
    public partial class frmConfig : frmBase
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            INIConfig.Write("Config", "VIDEO_HOST", this.txtVIDEO_HOST.Text);
            INIConfig.Write("Config", "VIDEO_PORT", this.txtVIDEO_PORT.Text);
            INIConfig.Write("Config", "VIDEO_USER", this.txtVIDEO_USER.Text);
            INIConfig.Write("Config", "VIDEO_PASS", this.txtVIDEO_PASS.Text);
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.txtVIDEO_HOST.Text = INIConfig.ReadString("Config", "VIDEO_HOST");
            this.txtVIDEO_PORT.Text = INIConfig.ReadString("Config", "VIDEO_PORT");
            this.txtVIDEO_USER.Text = INIConfig.ReadString("Config", "VIDEO_USER");
            this.txtVIDEO_PASS.Text = INIConfig.ReadString("Config", "VIDEO_PASS");
        }
    }
}
