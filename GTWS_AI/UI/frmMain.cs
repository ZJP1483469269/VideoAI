using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTWS;
using TLKJ_IVS;
using System.Collections;
using System.Runtime.InteropServices;
using TLKJ.Utils;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using Renci.SshNet;
using TLKJAI;
using GTWS_AI.UI;

namespace GTWS_TASK.UI
{
    public partial class frmMain : frmBase
    {
        public String ActiveCameraCode = "93748851157001000101#6cf5156252d2443bbe08944111478953";
        public String ActiveCameraName;
        public double X;
        public double Y;

        ulong ulReplayHandle = 0;
        ulong ulRealPlayHandle = 0;//实况
        ulong ulRealPlayCBHandle = 0;//实况码流 
        Boolean isPlay = false;

        static GTWS.IVS_API.EventCallBack eventCallBack = new GTWS.IVS_API.EventCallBack(EventCallBackFun);
        static GTWS.IVS_API.RealPlayCallBackRaw eventPlayCallBackRaw = new GTWS.IVS_API.RealPlayCallBackRaw(RealPlayCallBackRawFun);

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStartReal_Click(object sender, EventArgs e)
        {
            if (isPlay)
            {
                btnEndReal_Click(null, null);
            }

            IVS_REALPLAY_PARAM para = new IVS_REALPLAY_PARAM();
            para.bDirectFirst = false;
            para.bMultiCast = false;
            para.uiProtocolType = 2;
            para.uiStreamType = 1;
            int result = IVS_API.IVS_SDK_StartRealPlay(ApplicationEvent.iSession, ref para, ActiveCameraCode, pnlPlay.Handle, ref ulRealPlayHandle);
            if (result == 0)
            {
                isPlay = true;
                timAuto.Enabled = true;
            }
            else
            {
                isPlay = false;
                LB_MSG.Text = "播放实况失败";
                log4net.WriteLogFile("播放实况失败", LogType.ERROR);
            }
        }

        private void btnEndReal_Click(object sender, EventArgs e)
        {
            try
            {
                int iCode = IVS_API.IVS_SDK_StopRealPlay(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle);
                if (iCode == 0)
                {
                    isPlay = false;
                }
                else
                {
                    LB_MSG.Text = "停止实况失败";
                }
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile("停止实况失败", LogType.ERROR);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private IVS_CAMERA_BRIEF_INFO ActiveCamera;
        public void setView(int iTypeID)
        {
            pnlPlay.Visible = false;
            geckoWeb.Visible = false;
            picBox.Visible = false;

            if (iTypeID == 1)
            {
                pnlPlay.Visible = true;
            }
            else if (iTypeID == 2)
            {
                picBox.Visible = true;
                pnlPlay.Visible = true;
            }
        }
        public int InitCameraLogin()
        {
            //VIDEO_HOST=111.6.99.50
            //VIDEO_PORT=9900
            //VIDEO_USER=13569364016
            //VIDEO_PASS=13569364016

            //String VIDEO_HOST = "111.6.99.50";
            //String VIDEO_PORT = "9900";
            //String VIDEO_USER = "15090669997";
            //String VIDEO_PASS = "15090669997";

            String VIDEO_HOST = INIConfig.ReadString("Config", "VIDEO_HOST", "111.6.99.50");
            String VIDEO_PORT = INIConfig.ReadString("Config", "VIDEO_PORT", "9900");
            String VIDEO_USER = INIConfig.ReadString("Config", "VIDEO_USER", "15090669997");
            String VIDEO_PASS = INIConfig.ReadString("Config", "VIDEO_PASS", "15090669997");

            IVS_LOGIN_INFO info = new IVS_LOGIN_INFO();

            info.stIP.cIP = VIDEO_HOST;
            info.uiPort = (UInt32)StringEx.getInt(VIDEO_PORT);

            info.cUserName = VIDEO_USER;
            info.pPWD = VIDEO_PASS;
            info.stIP.uiIPType = 0;

            info.uiClientType = 0;
            info.uiLoginType = 0;
            int iCode = IVS_API.IVS_SDK_Init();
            iCode = IVS_API.IVS_SDK_Login(ref info, ref ApplicationEvent.iSession);
            if (iCode == 0)
            {
                log4net.WriteLogFile("用户登录视频服务器成功！");
            }
            else
            {
                log4net.WriteLogFile("IVS_SDK_Login:" + iCode + " 调用失败");
            }
            return iCode;
        }

        static private void EventCallBackFun(int iEventType, IntPtr pEventBuf, uint uiBufSize, IntPtr pUserData)
        {
            System.Console.WriteLine(iEventType.ToString());
            //txtEventType.Invoke(new Action(() => { txtEventType.Text = iEventType.ToString(); }));
            switch (iEventType)
            {
                case 10013:
                    IVS_ALARM_NOTIFY alarm = (IVS_ALARM_NOTIFY)Marshal.PtrToStructure(pEventBuf, typeof(IVS_ALARM_NOTIFY));
                    uint uiSize = (uint)Marshal.SizeOf(typeof(IVS_ALARM_EVENT));
                    IntPtr pAlarmEvent = Marshal.AllocHGlobal((int)uiBufSize);
                    IVS_API.IVS_SDK_GetAlarmEventInfo(ApplicationEvent.iSession, alarm.ullAlarmEventID, alarm.cAlarmInCode, pAlarmEvent);

                    long sizeInfo1 = Marshal.SizeOf(typeof(IVS_ALARM_NOTIFY));
                    long sizeInfo2 = Marshal.SizeOf(typeof(IVS_ALARM_OPERATE_INFO));
                    IntPtr tempInfoIntPtr = Marshal.AllocHGlobal((int)sizeInfo1);
                    byte[] tempInfoByte = new byte[sizeInfo1];

                    Marshal.Copy(pAlarmEvent, tempInfoByte, 0, (int)sizeInfo1);
                    Marshal.Copy(tempInfoByte, 0, tempInfoIntPtr, (int)sizeInfo1);

                    IVS_ALARM_NOTIFY NOTIFY = (IVS_ALARM_NOTIFY)Marshal.PtrToStructure(tempInfoIntPtr, typeof(IVS_ALARM_NOTIFY));
                    break;
                default:
                    break;
            }
        }
        static private void RealPlayCallBackRawFun(ref ulong ulHandle, ref IVS_RAW_FRAME_INFO pRawFrameInfo, IntPtr pBuf, IntPtr uiBufSize, IntPtr pUserData)
        {
            //uiStreamType  这个字段  如果是 PAY_LOAD_TYPE_H264  99  或者 PAY_LOAD_TYPE_MJPEG 26 为视频
            if (!(pRawFrameInfo.uiStreamType == 99 || pRawFrameInfo.uiStreamType == 26))
            {
                return;
            }

            byte[] bt = new byte[uiBufSize.ToInt32()];
            Marshal.Copy(pBuf, bt, 0, uiBufSize.ToInt32());
            FileStream outFileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "//11.264", FileMode.Append);
            using (BinaryWriter writer = new BinaryWriter(outFileStream))
            {
                writer.Write(bt);
            }
            outFileStream.Close();

        }


        private void btnPTZ_Down(object sender, MouseEventArgs e)
        {
            int iControlCode = 0;
            int pLockStatus = 0;
            switch ((sender as Button).Name)
            {
                case "btnLeft":
                    iControlCode = 4;
                    break;
                case "btnUP":
                    iControlCode = 2;
                    break;
                case "btnDown":
                    iControlCode = 3;
                    break;
                case "btnRight":
                    iControlCode = 7;
                    break;
                default:
                    break;


            }
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, iControlCode, "2", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteLogFile("IVS_SDK_PtzControl控制失败");
                MessageBox.Show("IVS_SDK_PtzControl控制失败");
            }
        }

        private void btnPTZ_UP(object sender, MouseEventArgs e)
        {
            int pLockStatus = 0;
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, 1, "2", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteLogFile("IVS_SDK_PtzControl控制失败");
                MessageBox.Show("IVS_SDK_PtzControl控制失败");
            }
        }

        private void btnLENS_PLUS_MouseDown(object sender, MouseEventArgs e)
        {
            int iControlCode = 0;
            int pLockStatus = 0;
            switch ((sender as Button).Name)
            {
                case "btnLENS_APERTURE_OPEN":
                    iControlCode = 21;
                    break;
                case "btnLENS_ZOOM_IN":
                    iControlCode = 23;
                    break;
                case "btnLENS_FOCAL_NEAT":
                    iControlCode = 25;
                    break;
                default:
                    break;
            }
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, iControlCode, "2", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteLogFile("IVS_SDK_PtzControl:" + iCode + " 调用失败");
                MessageBox.Show("IVS_SDK_PtzControl控制失败");
            }
        }

        private void btnLENS_MINUS_MouseDown(object sender, MouseEventArgs e)
        {
            int iControlCode = 0;
            int pLockStatus = 0;
            switch ((sender as Button).Name)
            {
                case "btnLENS_APERTURE_CLOSE":
                    iControlCode = 22;
                    break;
                case "btnLENS_ZOOM_OUT":
                    iControlCode = 24;
                    break;
                case "btnLENS_FOCAL_FAR":
                    iControlCode = 26;
                    break;
                default:
                    break;
            }
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, iControlCode, "2", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteLogFile("IVS_SDK_PtzControl:" + iCode + " 调用失败");
                MessageBox.Show("IVS_SDK_PtzControl控制失败");
            }
        }

        private void btnLENS_FOCAL_STOP_Click(object sender, EventArgs e)
        {
            int iControlCode = 0;
            int pLockStatus = 0;
            switch ((sender as Button).Name)
            {
                case "btnLENS_APERTURE_STOP":
                    iControlCode = 22;
                    break;
                case "btnLENS_ZOOM_STOP":
                    iControlCode = 24;
                    break;
                case "btnLENS_FOCAL_STOP":
                    iControlCode = 26;
                    break;
                default:
                    break;
            }
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, iControlCode, "3", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteLogFile("IVS_SDK_PtzControl" + iCode + "控制失败");
                MessageBox.Show((sender as Button).Name + "控制失败！");
            }
        }


        private void frmTask_Load(object sender, EventArgs e)
        {
            Application.Idle += onIdle_Event;
            try
            {
                picBox.Load(@"D:\VideoAI_1220\GTWS_TASK\Model\IMG06.jpg");
                geckoWeb.Navigate("http://www.baidu.com/");
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable dtInfo = null;
        Boolean LOAD_FLAG = false;
        private void onIdle_Event(object sender, EventArgs e)
        {
            if (!LOAD_FLAG)
            {
                LOAD_FLAG = true;
                InitCameraLogin();
                dtInfo = WebSQL.QueryData("SELECT * FROM XT_CAMERA WHERE DEVICE_ID='" + ActiveCameraCode + "'");
                this.Text = StringEx.getString(dtInfo, 0, "CAMERA_NAME");

                X = StringEx.GetDouble(dtInfo, 0, "X");
                Y = StringEx.GetDouble(dtInfo, 0, "Y");
                if (X > 0 && Y > 0)
                {
                    String cWebUrl = INIConfig.ReadString("Config", AppConfig.WEB_URL, "");
                    ApplicationEvent.Token = "644bc83a9e6a196cc48ff2603f21d4aa";
                    String cUrl = "http://" + cWebUrl + "/pages/webgis.aspx?TOKEN=" + ApplicationEvent.Token + "&TYPE_ID=2&X=" + X.ToString() + "&Y=" + Y.ToString();
                    geckoWeb.Navigate(cUrl);
                }
                btnStartReal_Click(null, null);
            }
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\AI\\";
            if (Directory.Exists(cAppDir))
            {
                Directory.CreateDirectory(cAppDir);
            }

            String cKeyID = AutoID.getAutoID();
            String cImageFileName = cAppDir + cKeyID + ".jpg";
            int iCode = IVS_API.IVS_SDK_LocalSnapshot(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle, 1, cImageFileName);
            if (iCode == 0)
            {

            }
            else
            {
                log4net.WriteLogFile("IVS_SDK_LocalSnapshot:" + iCode);
            }
        }

        private List<Rectangle> RectList = null;
        private int ActivePresetIndex = 0;

        private void timAuto_Tick(object sender, EventArgs e)
        {
            String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\AI\\";
            if (Directory.Exists(cAppDir))
            {
                Directory.CreateDirectory(cAppDir);
            }
            String cImageFileName = cAppDir + "01.jpg";
            ActiveImageName = cImageFileName;
            int iCode = IVS_API.IVS_SDK_LocalSnapshot(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle, 1, cImageFileName);
            if (iCode == 0)
            {
                Stream s = new FileStream(cImageFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Image img = new Bitmap(s);
                s.Close();
                picBox.Image = img;
                RectList = IMGAI.getImageRect(cImageFileName);
                String cStr = IMGAI.getImageText(ActiveImageName);
                if (!String.IsNullOrWhiteSpace(cStr))
                {
                    this.txtPTX.Text = cStr;
                }
                picBox.Image = IMGAI.getImage(cImageFileName.Replace("01.jpg", "02.jpg"));
            }
            else
            {
                log4net.WriteLogFile("IVS_SDK_LocalSnapshot:" + iCode);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

        }


        public String ActiveImageName;
        private void btnArc_Click(object sender, EventArgs e)
        {
            frmArea vDialog = new frmArea();
            if (vDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnXY_Click(object sender, EventArgs e)
        {
            GPSAI vAPI = new GPSAI();
            String cStr = this.txtPTX.Text;
            float fX = IMGAI.getX(cStr);
            float fP = IMGAI.getP(cStr);
            float fT = IMGAI.getT(cStr);
            vAPI.getCenter(ActiveCameraCode, fP, fT, fX);
        }
    }
}
