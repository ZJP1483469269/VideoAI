using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;

namespace WEB_TASK.UI
{
    public partial class frmDBConfig : Form
    {
        public frmDBConfig()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            INIConfig.Write("Config", AppConfig.DB_URL, this.txtDB_URL.Text);
            INIConfig.Write("Config", "DB_PREFIX", this.txtPrefix.Text);
            INIConfig.Write("Config", AppConfig.DB_TYPE, this.cmbDBType.SelectedText); 
        }

        private void frmDBConfig_Load(object sender, EventArgs e)
        {
            this.txtDB_URL.Text = INIConfig.ReadString("Config", AppConfig.DB_URL);
            this.txtPrefix.Text = INIConfig.ReadString("Config", "DB_PREFIX");
            cmbDBType.SelectedText = INIConfig.ReadString("Config", AppConfig.DB_TYPE);
        }
    }
}
