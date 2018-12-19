﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
namespace TLKJ.Utils
{
    /// <summary>
    /// WebUpload 的摘要说明
    /// </summary>
    public class WebUpload
    {
        public void Upload(String cFileName, String cUrl)
        {
            WebClient _webClient = new WebClient();
            //使用Windows登录方式
            _webClient.Credentials = new NetworkCredential("test", "123");
            //上传的链接地址（文件服务器）
            Uri _uri = new Uri(cUrl);
            //注册上传进度事件通知
            _webClient.UploadProgressChanged += WebUpload_ProgressChanged;
            //注册上传完成事件通知
            _webClient.UploadFileCompleted += WebUpload_Completed;
            //异步从D盘上传文件到服务器
            _webClient.UploadFileAsync(_uri, "PUT", cFileName);
            Console.ReadKey();
        }
        private static void WebUpload_Completed(object sender, UploadFileCompletedEventArgs e)
        {
            log4net.WriteLogFile("文件上传完成...");
        }

        //下载进度事件处理程序
        private static void WebUpload_ProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            log4net.WriteLogFile("数据正在处理中...");
        }
    }
}