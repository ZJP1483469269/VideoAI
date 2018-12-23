using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTWS_AI.UI
{
    public partial class WebGis : UserControl
    {

        private readonly string xulrunnerPath = Application.StartupPath + "/xulrunner";
        public WebGis()
        {
            InitializeComponent();

        }
        public void setCenter(float X, float Y)
        {

        }
        public void setUrl(String cUrl)
        {
            this.geckoWebBrowser1.Navigate(cUrl);
        }
    }
}
