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
using TLKJ.DB;
using Renci.SshNet;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GTWS_TASK.UI
{
    public partial class frmTask : frmBase
    {
        private ArrayList YWZ_VAL_LIST = new ArrayList();

        public ArrayList CAMEAR_VAL_LIST = new ArrayList();
        public ArrayList TASK_VAL_LIST = new ArrayList();

        Dictionary<String, String> ValDict = new Dictionary<string, string>();
        public String ActiveCameraText;
        public String ActiveCameraCode;
        public String ActiveCameraName;
        ulong ulReplayHandle = 0;
        ulong ulRealPlayHandle = 0;//实况
        ulong ulRealPlayCBHandle = 0;//实况码流 
        Boolean isPlay = false;

        static GTWS.IVS_API.EventCallBack eventCallBack = new GTWS.IVS_API.EventCallBack(EventCallBackFun);
        static GTWS.IVS_API.RealPlayCallBackRaw eventPlayCallBackRaw = new GTWS.IVS_API.RealPlayCallBackRaw(RealPlayCallBackRawFun);

        public frmTask()
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
            }
            else
            {
                isPlay = false;
                LB_MSG.Text = "播放实况失败";
                WriteLogText("播放实况失败");
            }
        }

        public void WriteLogText(String cStr)
        {
            log4net.WriteTextLog(ActiveCameraName + "[" + ActiveCameraCode + "]" + cStr);
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
                WriteLogText("停止实况失败");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private IVS_CAMERA_BRIEF_INFO ActiveCamera;
        private void timAfter_Tick(object sender, EventArgs e)
        {
            timAfter.Enabled = false;
            int iCode = 0;
            try
            {
                iCode = IVS_API.IVS_SDK_StopRealPlay(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle);
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("IVS_SDK_StopRealPlay失败！");
            }

            try
            {
                if (TASK_VAL_LIST.Count > 0)
                {
                    Object objInfo = TASK_VAL_LIST[TASK_VAL_LIST.Count - 1];
                    TASK_VAL_LIST.RemoveAt(TASK_VAL_LIST.Count - 1);
                    CameraNameList.SelectedIndex = TASK_VAL_LIST.Count - 1;

                    IVS_CAMERA_BRIEF_INFO rowKey = (IVS_CAMERA_BRIEF_INFO)objInfo;

                    IVS_REALPLAY_PARAM para = new IVS_REALPLAY_PARAM();
                    para.bDirectFirst = false;
                    para.bMultiCast = false;
                    para.uiProtocolType = 2;
                    para.uiStreamType = 1;
                    ActiveCameraCode = rowKey.cCode;
                    ActiveCameraName = rowKey.cName;

                    this.btnStartReal_Click(null, null);

                    ArrayList YWZList = Camera_YZW_List(ActiveCameraCode);
                    YWZ_TXT_LIST.Items.Clear();
                    YWZ_VAL_LIST.Clear();
                    //  for (int i = 0; (YWZList != null) && (i < YWZList.Count); i++)
                    if (YWZList.Count > 0)
                    {
                        for (int i = YWZList.Count - 1; i >= 0; i--)
                        {
                            IVS_PTZ_PRESET vPreset = (IVS_PTZ_PRESET)YWZList[i];
                            YWZ_TXT_LIST.Items.Add(vPreset.uiPresetIndex + ":" + vPreset.cPresetName);
                            YWZ_VAL_LIST.Add(vPreset);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog(ActiveCameraName + "[" + ActiveCameraCode + "]" + ":" + ex.Message);
            }
            finally
            {
                timPreset.Enabled = true;
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
                log4net.WriteTextLog("用户登录视频服务器成功！");
            }
            else
            {
                log4net.WriteTextLog("IVS_SDK_Login:" + iCode + " 调用失败");
            }
            return iCode;
        }
        public int Camera_GetCount()
        {
            IVS_INDEX_RANGE pIndexRange = new IVS_INDEX_RANGE();
            pIndexRange.uiFromIndex = 1;
            pIndexRange.uiToIndex = 1;
            int iCameraCount = 0;
            try
            {
                long sizeList = Marshal.SizeOf(typeof(IVS_CAMERA_BRIEF_INFO_LIST));//992
                long sizeInfo = Marshal.SizeOf(typeof(IVS_CAMERA_BRIEF_INFO));//980
                long deviceInfoLen = (pIndexRange.uiToIndex - pIndexRange.uiFromIndex) * sizeInfo;//2939020
                long iBuffSize = sizeList + deviceInfoLen;//2940012
                IntPtr pDeviceListPtr = Marshal.AllocHGlobal((int)iBuffSize);//734789664
                IntPtr pDeviceINFO = Marshal.AllocHGlobal((int)deviceInfoLen);//737738784

                int iCode = IVS_API.IVS_SDK_GetDeviceList(ApplicationEvent.iSession, 2, ref pIndexRange, pDeviceListPtr, (uint)iBuffSize);//0

                IVS_CAMERA_BRIEF_INFO_LIST list = new IVS_CAMERA_BRIEF_INFO_LIST();
                list.stCamerBriefInfo = new IVS_CAMERA_BRIEF_INFO[pIndexRange.uiToIndex - pIndexRange.uiFromIndex];
                list = (IVS_CAMERA_BRIEF_INFO_LIST)Marshal.PtrToStructure(pDeviceListPtr, typeof(IVS_CAMERA_BRIEF_INFO_LIST));
                iCameraCount = StringEx.getInt(list.uiTotal);//得到数量                
                Marshal.FreeHGlobal(pDeviceListPtr);
                Marshal.FreeHGlobal(pDeviceINFO);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
            return iCameraCount;
        }
        public void InitCamearaList(int iPACK_SIZE)
        {
            CAMEAR_VAL_LIST.Clear();
            CameraNameList.Items.Clear();

            IVS_INDEX_RANGE pIndexRange = new IVS_INDEX_RANGE();
            pIndexRange.uiFromIndex = 1;
            pIndexRange.uiToIndex = (uint)iPACK_SIZE;
            try
            {
                long sizeList = Marshal.SizeOf(typeof(IVS_CAMERA_BRIEF_INFO_LIST));//992
                long sizeInfo = Marshal.SizeOf(typeof(IVS_CAMERA_BRIEF_INFO));//980
                long deviceInfoLen = (pIndexRange.uiToIndex - pIndexRange.uiFromIndex) * sizeInfo;//2939020
                long iBuffSize = sizeList + deviceInfoLen;//2940012
                IntPtr pDeviceListPtr = Marshal.AllocHGlobal((int)iBuffSize);//734789664
                IntPtr pDeviceINFO = Marshal.AllocHGlobal((int)deviceInfoLen);//737738784

                int iRet = IVS_API.IVS_SDK_GetDeviceList(ApplicationEvent.iSession, 2, ref pIndexRange, pDeviceListPtr, (uint)iBuffSize);//0

                IVS_CAMERA_BRIEF_INFO_LIST list = new IVS_CAMERA_BRIEF_INFO_LIST();
                list.stCamerBriefInfo = new IVS_CAMERA_BRIEF_INFO[pIndexRange.uiToIndex - pIndexRange.uiFromIndex];
                list = (IVS_CAMERA_BRIEF_INFO_LIST)Marshal.PtrToStructure(pDeviceListPtr, typeof(IVS_CAMERA_BRIEF_INFO_LIST));
                int cCameraCount = StringEx.getInt(list.uiTotal);//得到数量
                if (list.uiTotal > 0)
                {
                    IntPtr tempInfoIntPtr = Marshal.AllocHGlobal((int)sizeInfo);//531396896
                    byte[] tempInfoByte = new byte[sizeInfo];

                    for (int index = -1; index < pIndexRange.uiToIndex - pIndexRange.uiFromIndex && index < list.uiTotal - 1; index++)
                    {
                        Marshal.Copy((IntPtr)(pDeviceListPtr + (int)sizeList + (int)sizeInfo * index), tempInfoByte, 0, (int)sizeInfo);
                        Marshal.Copy(tempInfoByte, 0, tempInfoIntPtr, (int)sizeInfo);

                        IVS_CAMERA_BRIEF_INFO st = (IVS_CAMERA_BRIEF_INFO)Marshal.PtrToStructure(tempInfoIntPtr, typeof(IVS_CAMERA_BRIEF_INFO));//摄像机的内容                
                        CAMEAR_VAL_LIST.Add(st);
                        CameraNameList.Items.Add(st.cCameraLocation);
                    }
                    Marshal.FreeHGlobal(tempInfoIntPtr);
                }
                else
                {

                }
                Marshal.FreeHGlobal(pDeviceListPtr);
                Marshal.FreeHGlobal(pDeviceINFO);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
        public ArrayList Camera_YZW_List(String cCameraID)
        {
            uint uiBufferSize = (uint)Marshal.SizeOf(typeof(IVS_PTZ_PRESET)) * 10;
            uint uiPTZPresetNum = 0; ;
            IntPtr pPTZPresetList = Marshal.AllocHGlobal((int)uiBufferSize);
            ArrayList KeyList = new ArrayList();
            int iCode = IVS_API.IVS_SDK_GetPTZPresetList(ApplicationEvent.iSession, cCameraID, pPTZPresetList, uiBufferSize, ref uiPTZPresetNum);
            if (iCode == 0)
            {
                IntPtr tempPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IVS_PTZ_PRESET)));
                byte[] tempByte = new byte[Marshal.SizeOf(typeof(IVS_PTZ_PRESET))];
                for (int index = 0; index < uiPTZPresetNum && index < 10; index++)
                {
                    Marshal.Copy(pPTZPresetList + index * Marshal.SizeOf(typeof(IVS_PTZ_PRESET)), tempByte, 0, Marshal.SizeOf(typeof(IVS_PTZ_PRESET)));
                    for (int i = 4; i < 88; i++)
                    {
                        if (tempByte[i] == 0)
                        {
                            for (int j = i; j < 88; j++)
                            {
                                tempByte[j] = 0;
                            }
                            break;
                        }
                    }
                    Marshal.Copy(tempByte, 0, tempPtr, Marshal.SizeOf(typeof(IVS_PTZ_PRESET)));
                    IVS_PTZ_PRESET preset = (IVS_PTZ_PRESET)Marshal.PtrToStructure(tempPtr, typeof(IVS_PTZ_PRESET));
                    KeyList.Add(preset);
                }

                Marshal.FreeHGlobal(tempPtr);
            }
            else
            {
                log4net.WriteTextLog("查询预置位失败");
            }
            return KeyList;
        }

        public int Camera_YZW_Remove(string cCameraID, uint iYZWIndex)
        {
            int iCode = IVS_API.IVS_SDK_DelPTZPreset(ApplicationEvent.iSession, cCameraID, iYZWIndex);
            if (iCode == 0)
            {
                MessageBox.Show("调用预置位成功！");
                return AppConfig.SUCCESS;
            }
            else
            {
                log4net.WriteTextLog("IVS_SDK_DelPTZPreset:" + iCode + " 调用失败");
                return AppConfig.FAILURE;
            }
        }

        public int Camera_YZW_AT(string cCameraID, uint iYZWIndex)
        {
            int pLockStatus = 0;
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, cCameraID, 11, iYZWIndex.ToString(), "3", ref pLockStatus);
            if (iCode == 0)
            {
                MessageBox.Show("调用预置位成功！");
                return AppConfig.SUCCESS;
            }
            else
            {
                log4net.WriteTextLog("IVS_SDK_PtzControl:" + iCode + " 调用失败");
                MessageBox.Show("调用预置位失败");
                return AppConfig.FAILURE;
            }
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

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfig vDialog = new frmConfig();
            try
            {
                vDialog.ShowDialog();
            }
            finally
            {
                vDialog.Close();
            }
        }

        private void CameraNameList_DoubleClick(object sender, EventArgs e)
        {
            if (CameraNameList.SelectedIndex != -1)
            {
                int iPos = CameraNameList.SelectedIndex;
                IVS_CAMERA_BRIEF_INFO vCameraInfo = (IVS_CAMERA_BRIEF_INFO)CAMEAR_VAL_LIST[iPos];
                timAfter.Enabled = false;
                ActiveCameraCode = vCameraInfo.cCode;
                ArrayList YZWList = Camera_YZW_List(ActiveCameraCode);

                YWZ_TXT_LIST.Items.Clear();
                YWZ_VAL_LIST.Clear();

                for (int i = 0; i < YZWList.Count; i++)
                {
                    IVS_PTZ_PRESET vPreset = (IVS_PTZ_PRESET)YZWList[i];
                    YWZ_TXT_LIST.Items.Add(vPreset.cPresetName);

                }
                this.btnStartReal_Click(null, null);
            }
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
                log4net.WriteTextLog("IVS_SDK_PtzControl控制失败");
                MessageBox.Show("IVS_SDK_PtzControl控制失败");
            }
        }

        private void btnPTZ_UP(object sender, MouseEventArgs e)
        {
            int pLockStatus = 0;
            int iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, 1, "2", "3", ref pLockStatus);
            if (iCode != 0)
            {
                log4net.WriteTextLog("IVS_SDK_PtzControl控制失败");
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
                log4net.WriteTextLog("IVS_SDK_PtzControl:" + iCode + " 调用失败");
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
                log4net.WriteTextLog("IVS_SDK_PtzControl:" + iCode + " 调用失败");
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
                log4net.WriteTextLog("IVS_SDK_PtzControl" + iCode + "控制失败");
                MessageBox.Show((sender as Button).Name + "控制失败！");
            }
        }

        private void 系统配置ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConfig vDialog = new frmConfig();
            try
            {
                vDialog.ShowInTaskbar = false;
                vDialog.ShowDialog();
            }
            finally
            {
                vDialog.Close();
            }
        }

        private void MUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCameraLogin();
            int iCameraCount = Camera_GetCount();
            int iCameraNum = StringEx.getInt(INIConfig.ReadString("Config", "VIDEO_MAX", "100"));
            if (iCameraCount > iCameraNum)
            {
                iCameraCount = iCameraNum;
            }
            if (iCameraCount > 0)
            {
                InitCamearaList(iCameraCount);
            }
            TASK_VAL_LIST = CAMEAR_VAL_LIST;
        }

        private void 遍历截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timAfter.Enabled = true;
        }

        private void btnPreset_Click(object sender, EventArgs e)
        {
            G_MSG.Clear();
            frmPreset vDialog = new frmPreset();
            try
            {
                vDialog.ShowInTaskbar = false;
                vDialog.ShowDialog();
            }
            finally
            {
                vDialog.Close();
            }

            if (G_MSG.Code == AppConfig.SUCCESS)
            {
                String cStr = G_MSG.cVal;
                uint uiPresetIndex = 0;
                int iCode = IVS_API.IVS_SDK_AddPTZPreset(ApplicationEvent.iSession, ActiveCameraCode, cStr, ref uiPresetIndex);
                if (iCode == 0)
                {
                    MessageBox.Show("预置位添加成功！");
                }
                else
                {
                    log4net.WriteTextLog("IVS_SDK_AddPTZPreset:" + iCode);
                    MessageBox.Show("预置位添加失败");
                }
            }
        }

        private void CameraYZWList_DoubleClick(object sender, EventArgs e)
        {
            int idx = YWZ_TXT_LIST.SelectedIndex;
            if (idx != -1)
            {
                Camera_YZW_AT(ActiveCameraCode, (uint)idx);
            }
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            INIConfig.setConfigFile(Application.StartupPath + @"\Config.ini");
            Application.Idle += new EventHandler(onIdle_Event);
            MUToolStripMenuItem_Click(null, null);
        }

        private void onIdle_Event(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\";
            //if (Directory.Exists(cAppDir))
            //{
            //    Directory.CreateDirectory(cAppDir);
            //}

            //String cKeyID = AutoID.getAutoID();
            //String cImageFileName = cAppDir + cKeyID + ".jpg";
            //int iCode = IVS_API.IVS_SDK_LocalSnapshot(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle, 1, cImageFileName);
            //if (iCode == 0)
            //{
            //    Image<Gray, byte> GrayImage = IVS_AI.getGrayImage(cImageFileName);
            //    String cAutoID = AutoID.getAutoID();
            //    GrayImage.Save("D:\\" + cAutoID + ".jpg");
            //    this.LB_MSG.Text = IVS_AI.getKeyText(GrayImage);
            //    //pictureBox1.Load("D:\\" + cAutoID + ".jpg");
            //}
            //else
            //{
            //    log4net.WriteTextLog("IVS_SDK_LocalSnapshot:" + iCode);
            //}
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\";
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
                log4net.WriteTextLog("IVS_SDK_LocalSnapshot:" + iCode);
            }
        }

        private int ActivePresetIndex = 0;
        private void timAI_Tick(object sender, EventArgs e)
        {
            //String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\";
            //if (Directory.Exists(cAppDir))
            //{
            //    Directory.CreateDirectory(cAppDir);
            //}
            //String cKeyID = AutoID.getAutoID() + "_" + String.Format("{0:0#00}", ActivePresetIndex);
            //String cFileName = cAppDir + cKeyID + ".jpg";

            //int iCode = IVS_API.IVS_SDK_LocalSnapshot(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle, 1, cFileName);
            //if (iCode == 0)
            //{
            //    Image<Gray, byte> GrayImage = IVS_AI.getGrayImage(cFileName);
            //    String cAutoID = AutoID.getAutoID();
            //    //GrayImage.Save("D:\\" + cAutoID + ".jpg");
            //    String cStr = IVS_AI.getKeyText(GrayImage);
            //    if (!String.IsNullOrWhiteSpace(cStr))
            //    {
            //        ValDict.Add(AppConfig.VALID, cStr);
            //    }
            //    else
            //    {
            //        timAI.Enabled = false;
            //    }
            //}
        }

        private void timTake_Tick(object sender, EventArgs e)
        {
            int iDFS_FLAG = StringEx.getInt(INIConfig.ReadString("UPLOAD", "DFS_FLAG", "0"));
            if (iDFS_FLAG > 0)
            {
                timTake.Enabled = false;
                try
                {
                    UploadTask.Execute();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    timTake.Enabled = true;
                }
            }
        }

        private void timPreset_Tick(object sender, EventArgs e)
        {
            timPreset.Enabled = false;
            int iCode = 0;
            try
            {
                Boolean AllowWait = false;
                int idx = -1;
                txtCAMERA_NAME.Text = ActiveCameraName;
                if (YWZ_VAL_LIST.Count > 0)
                {
                    idx = YWZ_VAL_LIST.Count - 1;

                    IVS_PTZ_PRESET vPreset = (IVS_PTZ_PRESET)YWZ_VAL_LIST[idx];
                    YWZ_VAL_LIST.RemoveAt(idx);
                    YWZ_TXT_LIST.Items.RemoveAt(idx);
                    int pLockStatus = 0;
                    iCode = IVS_API.IVS_SDK_PtzControl(ApplicationEvent.iSession, ActiveCameraCode, 11, vPreset.cPresetName, "3", ref pLockStatus);
                    if (iCode > 0)
                    {
                        log4net.WriteTextLog("调用预置位失败");
                        return;
                    }
                    else
                    {
                        AllowWait = true;
                    }
                }
                txtCAMERA_NAME.Text = ActiveCameraName + "[" + idx + "]";
                if (AllowWait)
                {
                    Boolean isAbort = false;
                    int iORD = 0;
                    while (!isAbort)
                    {
                        Application.DoEvents();
                        Thread.Sleep(200);
                        iORD++;
                        if (iORD > 15)
                        {
                            isAbort = true;
                        }
                    }
                }
                isPlay = true;
                String cAppDir = Path.GetDirectoryName(Application.ExecutablePath) + "\\Images\\";
                if (Directory.Exists(cAppDir))
                {
                    Directory.CreateDirectory(cAppDir);
                }


                String cKeyID = "";
                string cKeyGuid = "";
                if (idx != -1)
                {
                    cKeyID = AutoID.getAutoID() + "_" + String.Format("{0:0#00}", idx);
                    cKeyGuid = ActiveCameraCode + "X" + String.Format("{0:0#00}", idx);
                }
                else
                {
                    cKeyID = AutoID.getAutoID() + "_0000";
                    cKeyGuid = ActiveCameraCode + "X" + "0000";
                }

                String cFileName = cAppDir + cKeyID + ".jpg";
                Application.DoEvents();
                iCode = IVS_API.IVS_SDK_LocalSnapshot(ApplicationEvent.iSession, (UInt32)ulRealPlayHandle, 1, cFileName);
                DbManager.ExeSql(" insert into XT_CAMERA_STATUS(GUID,CAMERA_ID,PRESET_ID) VALUES('" + cKeyGuid + "','" + ActiveCameraCode + "'," + idx + 1 + ")");

                List<String> sqls = new List<string>();
                if (iCode == 0)
                {
                    String cDayTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    String cFileDir = "/Images/" + cKeyID + ".jpg";
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into XT_IMG_REC ");
                    sql.Append(" (REC_ID,CAMERA_ID,PRESET_ID,FILE_URL,CREATE_TIME,UPLOAD_FLAG )");
                    sql.Append(" values('" + cKeyID + "','" + ActiveCameraCode + "','" + idx + 1 + "','" + cFileDir + "','" + cDayTime + "',0)");
                    sqls.Add(sql.ToString());
                    sqls.Add(" UPDATE XT_CAMERA_STATUS SET FILE_URL='" + cFileDir + "',UPDATE_TIME='" + cDayTime + "' WHERE GUID='" + cKeyGuid + "'");
                    iCode = DbManager.ExeSql(sqls).Code;
                    if (iCode > 0)
                    {
                        log4net.WriteTextLog(cKeyID + "插入数据库成功");
                    }
                    log4net.WriteTextLog("IVS_SDK_LocalSnapshot成功！" + cFileName);
                }
                else
                {
                    log4net.WriteTextLog("IVS_SDK_LocalSnapshot失败！");
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                log4net.WriteTextLog("Camera_YZW_List失败！" + ex.Message);
            }
            finally
            {
                if (YWZ_VAL_LIST.Count > 0)
                {
                    timPreset.Enabled = true;
                }
                else if (YWZ_VAL_LIST.Count == 0)
                {
                    timAfter.Enabled = true;
                }
            }
        }



        private void timTask_Tick(object sender, EventArgs e)
        {
            Boolean AllowAMTask = INIConfig.ReadInt("TASK", AppConfig.DAY_AM + "_FLAG") > 0;
            Boolean AllowPMTask = INIConfig.ReadInt("TASK", AppConfig.DAY_PM + "_FLAG") > 0;
            String cDayHour = DateTime.Now.ToString("HHmmss");
            String cDay = DateTime.Now.ToString("yyyyMMdd");
            if (AllowAMTask)
            {
                String cTASK_AM = INIConfig.ReadString("TASK", AppConfig.DAY_AM);
                if (cTASK_AM.Equals(cDayHour))
                {
                    String cTASK_DAY = INIConfig.ReadString("TASK", AppConfig.DAY_AM + "_DAY");
                    if (!cTASK_DAY.Equals(cDay))
                    {
                        MUToolStripMenuItem_Click(null, null);
                        timAfter.Enabled = true;
                        INIConfig.Write("TASK", AppConfig.DAY_AM + "_DAY", cDay);
                    }
                }

            }
            if (AllowPMTask)
            {
                String cTASK_PM = INIConfig.ReadString("TASK", AppConfig.DAY_PM);
                if (cTASK_PM.Equals(cDayHour))
                {
                    String cTASK_DAY = INIConfig.ReadString("TASK", AppConfig.DAY_PM + "_DAY");
                    if (!cTASK_DAY.Equals(cDay))
                    {
                        MUToolStripMenuItem_Click(null, null);
                        timAfter.Enabled = true;
                        INIConfig.Write("TASK", AppConfig.DAY_PM + "_DAY", cDay);
                    }
                }
            }
        }

        private void 同步摄像机列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String cORG_ID = INIConfig.ReadString("Config", AppConfig.ORG_ID);
            JActiveTable aTable = new JActiveTable();
            for (int i = 0; i < TASK_VAL_LIST.Count; i++)
            {
                Object objInfo = TASK_VAL_LIST[TASK_VAL_LIST.Count - 1];
                TASK_VAL_LIST.RemoveAt(TASK_VAL_LIST.Count - 1);
                CameraNameList.SelectedIndex = TASK_VAL_LIST.Count - 1;

                IVS_CAMERA_BRIEF_INFO rowKey = (IVS_CAMERA_BRIEF_INFO)objInfo;

                aTable.TableName = "XT_CAMERA_LIST";
                aTable.ClearField();
                aTable.AddField("ID", rowKey.cCameraLocation.Split('-')[0]);
                aTable.AddField("DEVICE_ID", rowKey.cCode);
                aTable.AddField("ORG_ID", cORG_ID);
                aTable.AddField("ADDRESS", rowKey.cName);
                aTable.AddField("DEVICE_NAME", rowKey.cName);
                aTable.AddField("DEVGROUPCODE", rowKey.cDevGroupCode);
                aTable.AddField("NVRCODE", rowKey.cNvrCode);
                aTable.AddField("UISTATUS", rowKey.uiStatus);
                aTable.AddField("UITYPE", rowKey.uiType);
                aTable.AddField("UPDATE_TIME", DateUtils.getDayTimeNum());
                String sql = aTable.getUpdateSQL("DEVICE_ID='" + rowKey.cCode + "'");
                int iCode = DbManager.ExeSql(sql).Code;
                if (iCode > 0)
                {
                    log4net.WriteTextLog("摄像机更新成功");
                }
                else
                {
                    sql = aTable.getInsertSQL();
                    iCode = DbManager.ExeSql(sql).Code;
                    if (iCode > 0)
                        log4net.WriteTextLog("摄像机写入成功");
                    else
                        log4net.WriteTextLog("摄像机写入失败");

                }
            }
        }

        private void 任务时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDTConfig vDialog = new frmDTConfig();
            try
            {
                vDialog.ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
