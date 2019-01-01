using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using TLKJAI;

namespace GTWS_AI.UI
{
    public partial class frmArea : Form
    {
        int X = 220;
        int Y = 220;

        int iCX = 20;
        int iCY = 20;

        int iCW = 400;
        int iCH = 400;

        int iMAX = 300;

        Pen pen = new Pen(Color.Black, 1);
        Graphics g = null;
        public frmArea()
        {
            InitializeComponent();
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g = Graphics.FromHwnd(this.Handle);
        }

        private void DrawRect()
        {
            Point ptC = new Point(X, Y);
            Point ptCX = new Point(X + iMAX, Y);
            Point ptCH = new Point(X, Y - iMAX);
            g.DrawLine(pen, ptC, ptCH);
            g.DrawLine(pen, ptC, ptCX);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int iCameaHeight = StringEx.getInt(txtHeight.Text);
            Point ptCH = new Point(X, Y - iCameaHeight);
            int iJD = StringEx.getInt(txtJD.Text);
            Double dX = iCameaHeight * Math.Tan(Math.PI / (180 / iJD));
            int iX = (int)dX;
            Point ptX_W = new Point(X + iX, Y);
            txtGW.Text = dX.ToString();
            g.DrawLine(pen, ptCH, ptX_W);
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            DrawRect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Red, 1);
            Point ptC = new Point(X, Y);
            Point ptCH = new Point(X, Y - StringEx.getInt(txtHeight.Text));
            g.DrawLine(pen, ptC, ptCH);
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 2);
            g.DrawEllipse(pen, iCX, iCY, iCW, iCH);
        }

        private void DrawCircle(int iX, int iY, int iWidth)
        {
            Pen pen = new Pen(Color.Blue, 2);
            int i1x = iX - iWidth;
            int i1y = iY - iWidth;

            int i2x = iX + iWidth;
            int i2y = iY + iWidth;
            Point ptX = new Point(i1x, i1y);
            Point ptY = new Point(i2x, i2y);

            g.DrawEllipse(pen, new Rectangle(ptX, new Size(iWidth * 2, iWidth * 2)));
        }
        private void btnLine2_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 2);
            int icw = (int)StringEx.getFloat(this.txtGW.Text);
            int ich = (int)StringEx.getFloat(this.txtGW.Text);
            //g.DrawEllipse(pen, iCX, iCY, icw, ich);
            DrawCircle(X, Y, icw);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            int icw = (int)StringEx.getFloat(this.txtGW.Text);
            int iXZJD = (int)StringEx.getFloat(this.txtXZ.Text);

            Double dx = icw * Math.Cos(Math.PI / (180 / iXZJD));
            Double dy = icw * Math.Sin(Math.PI / (180 / iXZJD));

            //长度转经纬度
            //degree = meter / (2 * Math.PI * 6371004) * 360;

            //经纬度转长度米
            //meter = degree / 360 * 2 * (2 * Math.PI * 6371004;
            Point ptC = new Point(X + (int)dx, Y - (int)dy);

            Point ptCY = new Point(X + (int)dx, Y);

            Point ptCX = new Point(X, Y - (int)dy);

            // Point ptCX = new Point(X, (int)dy - Y);

            //  g.DrawLine(pen, ptC, ptCX);

            g.DrawLine(pen, ptC, ptCY);
            g.DrawLine(pen, ptC, ptCX);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GPSAI vAPI = new GPSAI(); 
        }
    }
}
