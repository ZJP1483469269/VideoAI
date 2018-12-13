using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using System.Net;
using System.IO;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;

namespace WEB_TASK
{
    public partial class frmMain : Form
    {
        List<KeyValue> ResultList = new List<KeyValue>();
        List<String> HrefList = new List<String>();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }
        private String getPageHtml(String cUrl)
        {
            try
            {
                WebClient vClient = new WebClient();
                vClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = vClient.DownloadData(cUrl); //从指定网站下载数据
                string cStr = Encoding.UTF8.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句 
                return cStr;
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog vDialog = new OpenFileDialog();
            if (vDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<String> UrlList = FileLib.getFileLines(vDialog.FileName);
                for (int i = 0; i < UrlList.Count; i++)
                {
                    TaskList.Items.Add(UrlList[i]);
                }
            }
        }
        private String getHtmlValue(String cPage, String cStr)
        {
            Document doc = NSoupClient.Parse(cPage);
            Elements rs = doc.Select(cStr);
            if (rs != null && rs.Count > 0)
            {
                return rs[0].Text();
            }
            else
            {
                return null;
            }
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            txtPageHtml.Text = getPageHtml(this.txtUrl.Text);
            Document doc = NSoupClient.Parse(txtPageHtml.Text);
            var rs = doc.Select(this.txtMatch.Text);

            for (int i = 0; i < rs.Count; i++)
            {
                var voKey = rs[i];
                String cLINK_TEXT = StringEx.getString(voKey.Text());
                if (!String.IsNullOrWhiteSpace(cLINK_TEXT))
                {
                    String cLINK_HREF = StringEx.getString(voKey.Attr("abs:href"));
                    if (HrefList.IndexOf(cLINK_HREF) == -1)
                    {
                        HrefList.Add(cLINK_HREF);
                        KeyValue rowKey = new KeyValue();
                        rowKey.Text = cLINK_TEXT;
                        rowKey.Val = cLINK_HREF;
                        String cPageHtml = getPageHtml(cLINK_HREF);
                        rowKey.Tag = getHtmlValue(cPageHtml, "#content");
                        ResultList.Add(rowKey);
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TaskList.Items.Count; i++)
            {
                String cStr = StringEx.getString(TaskList.Items[i]);
                this.txtUrl.Text = cStr;
                btnAnalyse_Click(null, null);
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ResultList.Count; i++)
            {
                String cLINK_TEXT = ResultList[i].Text;
                String cLINK_HREF = ResultList[i].Val;

                sb.AppendLine(cLINK_TEXT + ":" + cLINK_HREF);
            }
            this.txtPageUrl.Text = sb.ToString();
        }
    }
}
