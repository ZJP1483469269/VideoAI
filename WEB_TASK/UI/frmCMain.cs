﻿using System;
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
using System.Threading;
using System.Web.UI;
using Gecko;
using TLKJ_IVS;
using WEB_TASK.UI;

namespace WEB_TASK
{
    public partial class frmCMain : Form
    {
        List<JHref> ResultList = new List<JHref>();
        List<String> HrefList = new List<String>();

        public delegate void AppendHref_delegate(JHref rowKey);
        Sunisoft.IrisSkin.SkinEngine iskin = new Sunisoft.IrisSkin.SkinEngine();

        public frmCMain()
        {
            InitializeComponent();
            Xpcom.Initialize(Environment.CurrentDirectory + @"\xulrunner");
            iskin.SkinFile = "skins/PageColor2.ssk";
        }

        private void AppendHref(JHref rowKey)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.txtResult.InvokeRequired)
            {
                AppendHref_delegate d = new AppendHref_delegate(AppendHref);
                this.Invoke(d, new object[] { rowKey });
            }
            else
            {
                txtResult.Text = rowKey.UrlName;
            }
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

        JHref ActiveHref = null;
        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            JHref vTask = new JHref();
            vTask.Url = this.txtUrl.Text.Replace("{PAGE_NO}", "");
            vTask.UrlName = "0";
            vTask.Layer = 1;
            vTask.UrlID = StringEx.getString(ActiveHref.UrlID);
            vTask.Prefix = ActiveHref.Prefix;
            AddHref(1, vTask);
            Thread vThread = new Thread(ExecuteHref);
            vThread.Start();
        }

        public List<List<JHref>> PageList = new List<List<JHref>>();
        public int AddHref(int iLayer, JHref rowKey)
        {
            log4net.WriteLogFile("第" + iLayer + "层，添加链接" + rowKey.UrlName);
            AppendHref(rowKey);
            PageList[iLayer - 1].Add(rowKey);
            return PageList[iLayer - 1].Count - 1;
        }
        public int AddHref(int iLayer, List<JHref> KeyList)
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                JHref rowKey = KeyList[i];
                AddHref(iLayer, rowKey);
            }
            return PageList[iLayer - 1].Count - 1;
        }
        public void InitPageList(int iLayer)
        {
            for (int i = 0; i < iLayer; i++)
            {
                PageList.Add(new List<JHref>());
            }
        }
        public List<JHref> getPageHrefList(int iLayer)
        {
            return PageList[iLayer];

        }
        public List<JHref> getHerfList(JHref vTask)
        {
            String cUrl = vTask.Url;
            String cPageHtml = getPageHtml(cUrl);
            Document doc = NSoupClient.Parse(cPageHtml);
            var rs = doc.Select(ActiveHref.Match);
            List<JHref> KeyList = new List<JHref>();
            for (int i = 0; i < rs.Count; i++)
            {
                var voKey = rs[i];
                String cLINK_TEXT = StringEx.getString(voKey.Text());
                if (!String.IsNullOrWhiteSpace(cLINK_TEXT))
                {
                    String cLINK_HREF = StringEx.getString(voKey.Attr("abs:href"));
                    if (String.IsNullOrEmpty(cLINK_HREF))
                    {
                        continue;
                    }
                    if (!cLINK_HREF.StartsWith(vTask.Prefix))
                    {
                        continue;
                    }
                    if (HrefList.IndexOf(cLINK_HREF) == -1)
                    {
                        HrefList.Add(cLINK_HREF);
                        JHref rowKey = new JHref();
                        rowKey.UrlName = cLINK_TEXT;
                        rowKey.Prefix = vTask.Prefix;
                        rowKey.Url = cLINK_HREF;
                        rowKey.UrlID = vTask.UrlID;
                        log4net.WriteLogFile(cLINK_HREF + ":" + cLINK_TEXT);
                        rowKey.Layer = vTask.Layer + 1;
                        KeyList.Add(rowKey);
                    }
                }
            }
            return KeyList;
        }

        public String getPageContent(JHref vTask, String cExpr)
        {
            String cUrl = vTask.Url;
            String cHtml = getPageHtml(cUrl);
            return getMatchText(cHtml, cExpr);
        }

        public String getMatchText(String cHtml, String cExpr)
        {
            Document doc = NSoupClient.Parse(cHtml);
            var rs = doc.Select(cExpr);
            if (rs.Count > 0)
            {
                return rs[0].Text();
            }
            else
            {
                return null;
            }
        }
        int iReply = 1;
        int iMax = 5;

        private void btnStart_Click(object sender, EventArgs e)
        {
            int iLayer = StringEx.getInt(this.ActiveHref.Layer);
            InitPageList(iLayer);
            btnAnalyse_Click(null, null);
        }

        public void ExecuteHref()
        {
            while (iReply < iMax)
            {
                log4net.WriteLogFile("第层" + iReply + "任务，开始！");
                List<JHref> KeyList = PageList[iReply - 1];
                for (int i = KeyList.Count - 1; i >= 0; i--)
                {
                    JHref vTask = KeyList[i];
                    String cUrl = vTask.Url;
                    String cPageHtml = getPageHtml(cUrl);
                    List<JHref> rs = getHerfList(vTask);
                    AddHref(iReply + 1, rs);
                    try
                    {
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                log4net.WriteLogFile("第层" + iReply + "任务，结束！");
                PageList[iReply - 1].Clear();
                iReply++;
                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {

                }
            }
        }


        private string ExecuteScript(string cExpr)
        {
            return StringEx.getString(Eval.EvalString(cExpr));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            String cPAGE_VAL = ActiveHref.PageVal;
            int iLayer = StringEx.getInt(this.ActiveHref.Layer);
            for (int i = 0; i < iLayer; i++)
            {
                cPAGE_VAL = cPAGE_VAL.Replace("{PAGE_NO}", i.ToString());
                cPAGE_VAL = ExecuteScript(cPAGE_VAL);
                JHref vTask = new JHref();
                vTask.Url = this.txtUrl.Text;
                vTask.Url = vTask.Url.Replace("{PAGE_NO}", cPAGE_VAL);
                vTask.UrlName = "0";
                vTask.Layer = i + 1;
                vTask.Prefix = this.ActiveHref.Prefix;
                List<JHref> rs = getHerfList(vTask);
                for (int k = 0; k < rs.Count; k++)
                {
                    var voKey = rs[k];
                    String cLINK_TEXT = StringEx.getString(voKey.UrlName);
                    if (!String.IsNullOrWhiteSpace(cLINK_TEXT))
                    {
                        String cLINK_HREF = StringEx.getString(voKey.Url);
                        if (String.IsNullOrEmpty(cLINK_HREF))
                        {
                            continue;
                        }
                        if (!cLINK_HREF.StartsWith(vTask.Prefix))
                        {
                            continue;
                        }
                        if (HrefList.IndexOf(cLINK_HREF) == -1)
                        {
                            HrefList.Add(cLINK_HREF);
                            JHref rowKey = new JHref();
                            rowKey.UrlName = cLINK_TEXT;
                            rowKey.Prefix = vTask.Prefix;
                            rowKey.Url = cLINK_HREF;
                            rowKey.UrlID = vTask.UrlID;
                            log4net.WriteLogFile(cLINK_HREF + ":" + cLINK_TEXT);
                            rowKey.Layer = vTask.Layer + 1;
                        }
                    }
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.geckoWebBrowser1.Navigate("http://www.baidu.com/");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDBConfig vDialog = new frmDBConfig();
            if (vDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }
        String[] FileList = null;
        private void button6_Click(object sender, EventArgs e)
        {
            //JActiveTable aTable = new JActiveTable();
            //aTable.TableName = "XT_WEB_PAGE";
            //for (int i = lstUrls.Items.Count - 1; i >= 0; i--)
            //{
            //    JHref rowKey = (JHref)lstUrls.Items[i];

            //    aTable.AddField("Text", rowKey.UrlName);
            //    aTable.AddField("Url", rowKey.Url);
            //    aTable.AddField("UrlID", rowKey.UrlID);
            //    aTable.AddField("Html", this.getPageContent(rowKey, "#content"));
            //    aTable.AddField("pubDate", this.getPageContent(rowKey, "#pubDate"));
            //    String sql = aTable.getInsertSQL();
            //    //Object[] ParmList = aTable.getParmList();
            //    WebSQL.ExecSQL(sql);
            //}
        }

        private void btnConfigLayer_Click(object sender, EventArgs e)
        {
            frmRule vDialog = new frmRule();
            if (vDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveHref = new JHref();
                ActiveHref.UrlID = vDialog.txtUrlID.Text;

                ActiveHref.Layer = StringEx.getInt(vDialog.txtLayer.Text);

                ActiveHref.Match = vDialog.txtMatch.Text;

                ActiveHref.Prefix = vDialog.txtPrefix.Text;

                ActiveHref.PageVal = vDialog.txtPageVal.Text;

                ActiveHref.UrlName = vDialog.txtUrlName.Text;

                ActiveHref.Url = vDialog.txtUrlName.Text;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            String cUrl = this.txtUrl.Text;
            this.geckoWebBrowser1.Navigate(cUrl);

        }

        private void timAuto_Tick(object sender, EventArgs e)
        {
            String cPageHtml = geckoWebBrowser1.TextContent;
            Document doc = NSoupClient.Parse(cPageHtml);
            Elements rs = doc.Select(this.txtMatch.Text);
            for (int i = 0; i < rs.Count; i++)
            {
                NSoup.Nodes.Element rowKey = rs[i];
                String cUrl = rowKey.Attr(this.txtAttr.Text);
                log4net.WriteLogFile(cUrl);
            }

        }
        //public ChromiumWebBrowser chromeBrowser;

        //public void InitializeChromium()
        //{
        //    CefSettings settings = new CefSettings(); 
        //    // Create a browser component 
        //    chromeBrowser = new ChromiumWebBrowser("http://www.baidu.com/");

        //    // Add it to the form and fill it to the form window.
        //    this.Controls.Add(chromeBrowser);
        //    chromeBrowser.Dock = DockStyle.Fill;

        //} 
    }
}
