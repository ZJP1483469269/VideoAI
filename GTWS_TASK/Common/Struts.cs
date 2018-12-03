using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TLKJ_IVS
{
    #region 数据类型定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_LOGIN_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cUserName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string pPWD;

        [MarshalAs(UnmanagedType.Struct)]
        public IVS_IP stIP;

        public System.UInt32 uiPort;

        public System.UInt32 uiLoginType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cDomainName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cMachineName;

        public System.UInt32 uiClientType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    // IP信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_IP
    {
        public System.UInt32 uiIPType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cIP;
    };

    // 实况参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_REALPLAY_PARAM
    {
        public uint uiStreamType;       // 码流类型，值参考 IVS_STREAM_TYPE
        public uint uiProtocolType;     // 协议类型，1-UDP 2-TCP，默认为1
        public bool bDirectFirst;       // 是否直连优先，0-否 1-是，默认为0
        public bool bMultiCast;         // 是否组播，0-单播，1-组播，默认为0
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    // 分页信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_INDEX_RANGE
    {
        public uint uiFromIndex; // 开始索引
        public uint uiToIndex;   // 结束索引
    };

    // 视频子设备列表
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_BRIEF_INFO_LIST
    {
        public uint uiTotal;           // 总记录数
        public IVS_INDEX_RANGE stIndexRange;   // 分页信息
        [MarshalAs(UnmanagedType.ByValArray)]
        public IVS_CAMERA_BRIEF_INFO[] stCameraInf;
    }

    // 视频子设备列表简要信息（查询视频子设备列表）
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_BRIEF_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 192)]
        public string cName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cDevGroupCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cParentCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDomainCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDevModelType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cVendorType;

        public uint uiDevFormType;                              // 主设备类型：参考 IVS_MAIN_DEVICE_TYPE

        public uint uiType;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cCameraLocation;
        public uint uiCameraStatus;		                // 摄像机扩展状态：1 – 正常	2 – 视频丢失

        public uint uiStatus;			                // 设备状态：0-离线，1-在线，2-休眠 参考 IVS_DEV_STATUS

        public uint uiNetType;                          // 网络类型 0-有线  1-无线, 参考 IVS_NET_TYPE
        public bool bSupportIntelligent;                // 是否支持智能分析  0-不支持 1-支持

        public bool bEnableVoice;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cDevCreateTime;

        public bool bIsExDomain;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cDevIp;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RAW_FRAME_INFO
    {
        public uint uiStreamType;		// 编码格式，参考：IVS_PAYLOAD_TYPE
        public uint uiFrameType;		// 帧数据类型，SPS、PPS、IDR、P（视频数据有效）
        public uint uiTimeStamp;		// 时间戳
        public UInt64 ullTimeTick;		// 绝对时间戳
        public uint uiWaterMarkValue;	// 水印数据 ，0表示无水印数据（视频数据有效）

        public uint uiWidth;			// 视屏宽度
        public uint uiHeight;			// 视频高度

        public uint uiGopSequence;		// GOP序列
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IVS_LOCAL_RECORD_PARAM
    {
        public uint uiRecordFormat;                 // 录像文件格式，参考 IVS_RECORD_FILE_TYPE
        public uint uiSplitterType;                 // 录像分割方式，参考 IVS_RECORD_SPLITE_TYPE
        public uint uiSplitterValue;                // 录像分割值，文件分割方式是时间时，填入时间，对应单位为分钟，5-60分钟，同时满足文件大小不超过2048MB的限制，文件分割方式是容量时，填入容量，对应单位为M，10-2048MB

        public uint uiDiskWarningValue;             // 本地录像，磁盘空间小于此值告警，单位M（进行“本地录像通用事件上报”2-本地录像告警）
        public uint uiStopRecordValue;              // 本地录像，磁盘空间小于此值停止录像，单位M（进行“本地录像通用事件上报”3-本地录像磁盘满停止）

        public uint uiRecordTime;                   // 录像时长，单位秒，整数，300 ~ 43200（12小时）

        public bool bEncryptRecord;    // 录像是否加密
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string cRecordPWD;   // 本地录像密码

        public uint uiNameRule;                     // 录像文件命名规则，参考 IVS_RECORD_NAME_RULE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cSavePath;   // 本地录像存放路径，加上文件名长度不超过256字节
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;                   // 保留字段
    };
    // 业务告警通知信息(平台通知客户端)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_ALARM_NOTIFY
    {
        public UInt64 ullAlarmEventID;                      // 告警事件ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cAlarmInCode;	                        // 告警源编码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDevDomainCode;                       // 设备所属域编码
        public UInt32 uiAlarmInType;					    // 告警源类型 IVS_ALARM_IN_TYPE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cAlarmInName;                         // 告警源名称

        public UInt32 uiAlarmLevelValue;		            // 告警级别权值,1~100
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cAlarmLevelName;	                    // 告警级别名称,汉字和字母（a-z和A-Z），数字，中划线和下划线，1~20个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string cAlarmLevelColor;			            // 告警级别颜色,使用颜色字符串表示ARGB,例如#FFFF0000 表示红色，不透明
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cAlarmType;	                        // 告警类型
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cAlarmTypeName;	                    // 告警类型名称，汉字和字母（a-z和A-Z），数字，中划线和下划线，1~64个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string cAlarmCategory;				        // 告警类型类别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cOccurTime;                           // 告警发生时间：yyyyMMddHHmmss
        public UInt32 uiOccurNumber;		                // 告警发生次数
        public UInt32 uiAlarmStatus;		                // 告警状态 参考 IVS_ALARM_STATUS

        public bool bIsCommission;	                        // 是否为授权告警信息：0-否，1-是
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cPreviewUrl;	                        // 在存在联动抓拍或者智能分析时的图片预览URL

        public bool bExistsRecord;	                        // 是否存在告警录像：0-否，1-是
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;                             // NVR编码，可用于更新NVR路由
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserver;                            // 保留字段
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string cAlarmDesc;	                        // 告警描述信息，键盘可见字符和中文，0~256字符。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string cExtParam;                            // 扩展参数
    };

    // 时间片段结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_TIME_SPAN
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cStart; // 格式如yyyyMMddHHmmss
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cEnd; // 格式如yyyyMMddHHmmss
    }

    // 录像信息列表
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct IVS_RECORD_INFO_LIST
    {
        public uint uiTotal;               // 总记录数
        IVS_INDEX_RANGE stIndexRange;      // 分页信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cRes;	//保留字段
        [MarshalAs(UnmanagedType.ByValArray)]
        public IVS_RECORD_INFO[] stRecordInfo; // 录像检索信息
    };

    // 录像检索信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RECORD_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cRecordFileName;	   // 录像文件名
        public uint uiRecordMethod;		   // 录像存储位置：参考 IVS_RECORD_METHOD
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;           // NVR编码，仅在平台录像检索结果中带此字段（内部处理）
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cMBUDomain;        //备份服务器域编码备份录像检索结果中带此字段
        public uint uiRecordType;		 // 录像类型：参考 IVS_RECORD_TYPE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string cAlarmType;		// 告警类型，录像类型为告警录像时有效（英文字符串）
        public IVS_TIME_SPAN stTime;     // 录像起止时间
        public uint uiFrameExtractionTimes;	// 录像抽帧次数：参考 IVS_FRAME_EXTRACTION_TIMES
        public IVS_RECORD_BOOKMARK_INFO stBookmarkInfo;  // 录像标签信息，查询方式为按书签时有效
        public IVS_RECORD_LOCK_INFO stLockInfo;		 // 锁定信息，查询方式为按锁定状态时有效
        public IVS_RECORD_PTZ_PRESET_INFO stPtzPresetInfo; // 录像预置位信息，查询方式为按预置位查询时有效
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;  // 保留字段
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RECORD_BOOKMARK_INFO
    {
        public uint uiBookmarkID;                           // 录像标签ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 612)]
        public string cBookmarkName;   // 标签名，长度最大150字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cBookmarkTime;            // 标签时间：yyyyMMddHHmmss
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cCameraCode;          // 摄像头编码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cCameraName;              // 摄像头名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cUserDomain;       // 操作用户所在域编码
        public uint uiBookmarkCreatorID;                    // 创建书签的用户ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cBookmarkCreatorName;     // 创建书签的用户名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cNvrCode;             // NVR编码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cMBUDomain;        // 备份服务器域编码备份录像检索结果中带此字段
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RECORD_LOCK_INFO
    {
        public uint uiLockID;						        // 锁定记录ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cLockTime;		        // 执行锁定操作的时间
        public IVS_TIME_SPAN stLockTimeSpan;				        // 锁定的录像时段
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cLockDesc;	// 锁定描述
        public uint uiOperatorID;						    // 执行锁定操作的用户ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cOperatorName;            // 执行锁定操作的用户名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;                           // 保留字段
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RECORD_PTZ_PRESET_INFO
    {
        public uint uiID;	// 预置位ID
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cName;	// 预置位名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;                           // 保留字段
    };
    // 录像下载参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_DOWNLOAD_PARAM
    {
        public uint uiRecordFormat;                 // 录像文件格式，参考 IVS_RECORD_FILE_TYPE
        public uint uiSplitterType;                 // 录像分割方式，参考 IVS_RECORD_SPLITE_TYPE
        public uint uiSplitterValue;                // 录像分割值，文件分割方式是时间时，填入时间，对应单位为分钟，5-60分钟，同时满足文件大小不超过2048MB的限制，文件分割方式是容量时，填入容量，对应单位为M，1-2048MB

        public uint uiDiskWarningValue;             // 磁盘空间小于此值告警，单位M（进行“本地录像通用事件上报”2-本地录像告警）
        public uint uiStopRecordValue;              // 磁盘空间小于此值停止录像，单位M（进行“本地录像通用事件上报”3-本地录像磁盘满停止）

        public uint uiDownloadSpeed;                // 录像下载速度：0-不限速（全速），1-1倍速（前端只支持：1）
        public IVS_TIME_SPAN stTimeSpan;                   // 录像下载时间段

        public bool bEncryptRecord;                 // 录像下载是否加密
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string cRecordPWD;   // 录像下载密码
        public uint uiNameRule;                     // 录像文件命名规则，参考 IVS_RECORD_NAME_RULE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cSavePath;   // 录像存放路径，加上文件名长度不超过256字节
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cFileName;   // 本地录像文件名，绝对路径，长度为256字节（包括后缀和结束符），多份录像文件用序号区分，为空时，根据uiNameRule的命名规则和cSavePath的路径确认文件名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;                   // 保留字段
    };
    // 录像回放参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_PLAYBACK_PARAM
    {
        public uint uiProtocolType;     // 协议类型，1-UDP 2-TCP，默认为1
        public IVS_TIME_SPAN stTimeSpan;         // 实况启动、结束时间
        public float fSpeed;             // 回放速率
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;                   // 保留字段
    };
    public enum IVS_CONFIG_TYPE
    {
        CONFIG_DEVICE_CFG = 1,                    // 主设备基本参数 IVS_DEVICE_CFG
        CONFIG_DEVICE_NET_CFG,                  // 主设备网络参数 IVS_DEVICE_NET_CFG
        CONFIG_DEVICE_TIME_CFG,                 // 主设备时间参数（NTP、时区）IVS_DEVICE_TIME_CFG

        CONFIG_CAMERA_CFG,                      // 摄像机基本参数设置 IVS_CAMERA_CFG
        CONFIG_CAMERA_STREAM_CFG,               // 摄像机码流参数设置 IVS_CAMERA_STREAM_CFG
        CONFIG_CAMERA_DISPLAY_CFG,              // 摄像机基本显示参数设置 IVS_CAMERA_DISPLAY_CFG
        CONFIG_CAMERA_IMAGING_CFG,              // 摄像机图像参数设置 IVS_CAMERA_IMAGING_CFG
        CONFIG_CAMERA_OSD_CFG,                  // 摄像机OSD参数设置 IVS_CAMERA_OSD_CFG
        CONFIG_CAMERA_MOTION_DETECTION_CFG,     // 摄像机运动检测参数设置 IVS_MOTION_DETECTION
        CONFIG_CAMERA_VIDEO_HIDE_ALARM_CFG,     // 摄像机遮挡告警参数设置 IVS_VIDEO_HIDE_ALARM
        CONFIG_CAMERA_VIDEO_MASK_CFG,           // 摄像机隐私保护参数设置 IVS_VIDEO_MASK
        CONFIG_CAMERA_AUDIO_CFG,                // 摄像机音频参数设置 IVS_AUDIO_CFG

        CONFIG_SERIAL_CHANNEL_CFG,              // 摄像机串口参数设置 IVS_SERIAL_CHANNEL_CFG
        CONFIG_ALARM_IN_CFG,                    // 告警输入子设备设置 IVS_ALARM_IN_CFG
        CONFIG_ALARM_OUT_CFG,                   // 告警输出子设备设置 IVS_ALARM_OUT_CFG

        CONFIG_DEVICE_PTZ_CFG,                  // 云台参数设置 IVS_PTZ_CFG
        CONFIG_CAMERA_EXTEND_CFG,               // 摄像机扩展参数设置 IVS_CAMERA_EXTEND_CFG

        CONFIG_CAMERA_SNAPSHOT_CFG,             // 摄像机抓拍参数设置 IVS_CAMERA_SNAPSHOT_CFG
        CONFIG_CAMERA_RECORD_STREAM_CFG,        // 摄像机前端录像码流设置 IVS_CAMERA_RECORD_STREAM_CFG
    };
    // 基本数据结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_RECT_FLOAT
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_OSD_NAME
    {
        public bool bEnableOSDName;                     // 是否显示文字：0-不显示，1-显示
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cOSDNameText; // 文字内容
        public IVS_RECT_FLOAT rectText;                     // 文字区域

        public bool bEnableSwitch;		                // 是否交替显示：0-不交替，1-交替
        public uint uiSwitchInterval;                   // 交替显示时间间隔，单位为秒

        public bool bEnableTextBlink;		            // 是否允许闪烁：0-不闪烁，1-闪烁
        public bool bEnableTextTranslucent;	            // 是否允许透明：0-不透明，1-透明
        public uint uiTextTranslucentPercent;           // 文字透明度：0-100
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;
    };
    // 摄像机OSD参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CAMERA_OSD_CFG
    {
        public bool bEnableOSD; // 是否启用：0-停用 1-启用
        public IVS_OSD_TIME stOSDTime;  // OSD时间信息
        public IVS_OSD_NAME stOSDName;  // OSD文字信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    // 前端OSD时间
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_OSD_TIME
    {
        public bool bEnableOSDTime;     // 是否显示时间：0-不显示，1-显示
        public uint uiTimeFormat;       // 时间显示格式：？1: XXXX-XX-XX XX:XX:XX(如2009-09-09 09:09:09), 2: XXXX年XX月XX日 XX :XX :XX(2009年09月09日 09 :09 :09；3: UTC时间
        public float fTimeX;             // 时间X坐标，以左上角为原点
        public float fTimeY;             // 时间Y坐标，以左上角为原点
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_USER_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cDomainCode;	// 用户所属域编码
        public uint uiUserID;						// 用户ID，系统分配
        public uint bFirstLogin;					// 是否是第一次登录
        // 添加用户需要的信息
        public uint uiRoleID;						// 角色ID，管理员配置：结构定义参考 IVS_USER_ROLE
        public uint uiGroupID;						// 用户组ID：结构定义参考 IVS_USER_GROUP
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cLoginName;		// 登录账号：登录账号，字母（a-z和A-Z）,数字，中划线和下划线，1~20个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cPWD;	// 登录密码：登录密码，键盘可见字符，1~16个字符。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cPwdModifyDate;	// 密码修改日期

        public uint uiUserType;						// 用户类型：0：IVS普通用户，1：Windows域用户
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cUserDomain;	// Windows域名

        public uint uiStatus;						// 用户状态：参考 IVS_ACCOUNT_STATUS
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cRegiterDate;		// 创建日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string cUserDesc;	// 用户描述，键盘可见字符和中文，0~64字符。
        public uint uiPTZLevel;						// 云镜控制优先级：0~9
        public uint uiMaxSessionCnt;				// 多点登录数,最大长度3字符，数字
        public uint uiMaxVideoCnt;					// 最大视频路数，最大长度3字符，数字 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cLockTime;		// 账号被锁定的时间

        // 个人基本信息，用于个人修改
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cUserName;		// 真实姓名，汉字和字母（a-z和A-Z），数字，中划线和下划线，1~20个字符
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cEmail;			// 邮箱，参考网易163邮箱限制。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cPhone;	// 电话，0~25位数字，中划线
        public uint uiSex;							// 性别：0-女性，1-男性

        public uint uiValidDateFlag;                     //是否有账户有效期(1:有;0:没有)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cValidDateStart;       //账户有效起始日期 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cValidDateEnd;         //账户有效结束日期

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string cReserve;                       // 保留字段
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_PTZ_PRESET
    {
        public uint uiPresetIndex;   // 预置位索引
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
        public string cPresetName;   // 预置位名称，1~20个字符。
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    // 告警处理信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_ALARM_OPERATE_INFO
    {
        public uint uiOperatorID;	                // 处理人ID 参考 IVS_USER_INFO
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string cOperatorName;	// 处理人名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string cOperateTime;		// 告警处理时间
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserver;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string cOperateInfo;	// 告警处理人员输入的信息
    };
    // 告警信息（告警事件查询）
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_ALARM_EVENT
    {
        public IVS_ALARM_NOTIFY stAlarmNotify;      // 告警信息
        public IVS_ALARM_OPERATE_INFO stOperateInfo;	    // 告警处理信息
    };
    // 云镜巡航轨迹点
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CRUISE_TRACK_POINT
    {
        public uint uiPresetIndex;  // 预置位索引, 最多20个点
        public uint uiDwellTime;    // 预置位停留时间，秒为单位，1~3600秒 
        public uint uiSpeed;        // 云台速度，1~10 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IVS_CRUISE_TRACK
    {
        public uint uiCruiseNO;                                             // 轨迹索引
        public uint uiCruiseType;                                           // 巡航类型，值参考 IVS_CRUISE_TYPE
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
        public string cTrackName;                  // 轨迹名称
        public uint uiTrackPointNum;                                        // 轨迹点个数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public IVS_CRUISE_TRACK_POINT[] stTrackPoin;   // 轨迹点列表
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string cReserve;
    };
    // 录像下载信息
    public struct IVS_DOWNLOAD_INFO
    {
        public uint uiDownloadStatus;   // 下载状态，0-暂停 1-下载
        public uint uiDownloadSpeed;    // 下载速度，单位KB/S
        public ulong uiDownloadSize;     // 已下载大小，单位KB
        public uint uiTimeLeft;         // 剩余下载时间：单位秒
        public uint uiProgress;         // 当前下载进度：0-100，表示百分比
    };
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void RealPlayCallBackRaw(ref ulong ulHandle, ref IVS_RAW_FRAME_INFO pRawFrameInfo, IntPtr pBuf, IntPtr uiBufSize, IntPtr pUserData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void EventCallBack(int iEventType, IntPtr pEventBuf, uint uiBufSize, IntPtr pUserData);
    #endregion
}
