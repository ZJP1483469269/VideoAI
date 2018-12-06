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
    public partial class frmPreset : frmBase
    {
        public frmPreset()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            String cStr = this.txtPresetName.Text;
            if (String.IsNullOrEmpty(cStr))
            {
                MessageBox.Show("必须输入预制位的名称！");
                return;
            }
            G_MSG.setVal(cStr);
            this.Close();
        }
    }
}
