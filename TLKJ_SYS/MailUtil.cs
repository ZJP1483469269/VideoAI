using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using TLKJ.DAO;
/// <summary>
/// AppManager 的摘要说明
/// </summary>
/// 
namespace TLKJ.SYS
{
    public class MailUtil
    {
        public static object objLock = new object();
        public static Boolean SendMail(String cMail, String cTitle, String cPageHtml)
        {
            Email email = new Email();
            email.mailFrom = "6163440@163.com";
            email.mailPwd = "woainiNIAIWOME";
            email.mailSubject = cTitle;
            email.mailBody = cPageHtml;//邮件内容
            email.isbodyHtml = true;    //是否是HTML
            email.host = "smtp.163.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            email.mailToArray = new string[] { cMail };//接收者邮件集合
            //email.mailCcArray = new string[] {  };//抄送者邮件集合
            Email_sendDao eSend = new Email_sendDao();
            return eSend.Send(email);
        }
    }
}