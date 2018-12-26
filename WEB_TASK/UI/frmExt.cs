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
    public partial class frmExt : Form
    {
        public frmExt()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            INIConfig.Write("Config", "FileExt", this.txtFileExt.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
