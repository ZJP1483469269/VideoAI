using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TLKJ.Utils
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum IMMessageType
    {
        /// <summary>
        /// 请求
        /// </summary>
        Request,
        /// <summary>
        /// 响应
        /// </summary>
        Response,
        /// <summary>
        /// 异常
        /// </summary>
        Exception,
        Message
    }
}