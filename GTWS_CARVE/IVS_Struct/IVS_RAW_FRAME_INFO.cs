using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GTWS
{
    // 实况参数
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
}
