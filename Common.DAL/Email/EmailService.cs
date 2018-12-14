using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using TLKJ.Utils;
namespace TLKJ.DAO
{
    class EmailService:IService
    {

        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
            readQueryString();
        }
        public void emailSend()
        {
            String emailName = StringEx.getString(request["Values"]);

            Email email = new Email();
       
            email.mailFrom = "6163440@163.com";
            email.mailPwd = "woainiNIAIWOME";
            email.mailSubject = "1";
            email.mailBody = "获取发送的内容";
            email.isbodyHtml = true;    //是否是HTML
            email.host = "smtp.163.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            email.mailToArray = new string[] {emailName};//接收者邮件集合
            email.mailCcArray = new string[] {emailName};//抄送者邮件集合
            object obj = new object();
            Email_sendDao eSend = new Email_sendDao();
            response.Write(eSend.Send(email));
        }
    }
  

}
