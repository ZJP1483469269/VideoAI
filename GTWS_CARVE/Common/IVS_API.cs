using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using TLKJ_IVS;

namespace GTWS
{
    public class IVS_API
    {


        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_Init();
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_Login(ref IVS_LOGIN_INFO pLoginReqInfo, ref int piSessionID);
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetDomainRoute(int iSessionID, IntPtr pDomainRouteList, uint uiBufferSize);
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetDomainDeviceList(int iSessionID, string pDomainCode, uint uiDeviceType, ref IVS_INDEX_RANGE indexRange, IntPtr pDeviceList, uint uiBufferSize);
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetDeviceGroup(int iSessionID, string pDomainCode, string pDevGroupCode, IntPtr pDeviceGroupList, uint uiBufferSize);
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_Logout(int iSessionID);
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_Cleanup();


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void RealPlayCallBackRaw(ref ulong ulHandle, ref IVS_RAW_FRAME_INFO pRawFrameInfo, IntPtr pBuf, IntPtr uiBufSize, IntPtr pUserData);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void EventCallBack(int iEventType, IntPtr pEventBuf, uint uiBufSize, IntPtr pUserData);

        /// <summary>
        /// 关闭视频流
        /// </summary>
        /// <param name="iSessionID"></param>
        /// <param name="ulHandle"></param>
        /// <returns></returns>
        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_StopRealPlay(int iSessionID, UInt32 ulHandle);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_StartRealPlay(int p, ref IVS_REALPLAY_PARAM para, string ActiveCameraCode, IntPtr intPtr, ref ulong ulRealPlayHandle);


        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_LocalSnapshot(int iSessionID, UInt32 ulPlayHandle, uint uiPictureFormat, string pFileName);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_SubscribeAlarm(int iSessionID, string pReqXml);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_StartLocalRecord(int iSessionID, ref IVS_LOCAL_RECORD_PARAM pRecordParam, UInt32 ulPlayHandle, string pSaveFileName);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetDeviceList(int iSessionID, uint uiDeviceType, ref IVS_INDEX_RANGE pIndexRange, IntPtr pDeviceList, uint uiBufferSize);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_SetDeviceConfig(int iSessionID, string pDevCode, uint uiConfigType, IntPtr pBuffer, uint uiBufSize);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetAlarmEventInfo(int iSessionID, ulong ullAlarmEventID, string pAlarmInCode, IntPtr pAlarmEvent);


        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_PtzControl(int iSessionID, string pCameraCode, int iControlCode, string pControlPara1, string pControlPara2, ref int pLockStatus);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_GetPTZPresetList(int iSessionID, string pCameraCode, IntPtr pPTZPresetList, uint uiBufferSize, ref uint uiPTZPresetNum);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_DelPTZPreset(int iSessionID, string pCameraCode, uint ulPresetIndex);

        [DllImport(@"IVS_SDK.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int IVS_SDK_AddPTZPreset(int iSessionID, string pCameraCode, string pPresetName, ref uint uiPresetIndex); 
    }
}
