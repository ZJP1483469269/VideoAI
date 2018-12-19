using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils; 
using System.Runtime.InteropServices;

namespace GTWS_TASK.UI
{
    public partial class frmImage : Form
    {
        String cFileName = @"IMG01.jpg";
        private PictureBox ActiveBox = null;
        public frmImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MIN, "0"));
            int iMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.IMAGE_MAX, "0"));

            int iGrayMinVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MIN, "0"));
            int iGrayMaxVal = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.GRAY_MAX, "0"));
            int iEXPORT_IMAGE = StringEx.getInt(INIConfig.ReadString("Config", AppConfig.EXPORT_IMAGE, "0"));

            String cDFS_PATH = INIConfig.ReadString("UPLOAD", "DFS_PATH", "0");

            String cAppDir = Application.StartupPath;
            Boolean isUpload = false;
            JActiveTable aMaster = new JActiveTable();
            JActiveTable aSlave = new JActiveTable();
            aSlave.TableName = "XT_IMG_LIST";
            aMaster.TableName = "XT_IMG_REC"; 
             
        }

        private void frmImage_Load(object sender, EventArgs e)
        {
            ActiveBox = new PictureBox();
            ActiveBox.Parent = this;
            ActiveBox.Left = 10;
            ActiveBox.Top = 10;
            ActiveBox.Height = this.Height - 60;
            ActiveBox.Width = this.Width - 160;
            ActiveBox.BorderStyle = BorderStyle.FixedSingle;
            ActiveBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ActiveBox.Load(cFileName);
        }
    }
}
