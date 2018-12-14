using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using TLKJ.Utils;
using TLKJ.DB;
using System.Net.Mail;
using System.Web;

namespace TLKJ.DAO
{
    public class Email_sendDao:BaseDao<Email>
    { 
        public void init(HttpRequest res, HttpResponse rep)
        {
            request = res;
            response = rep;
        }

        public bool Send(Email email)
        {
            //Email email = new Email();
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(email.mailFrom);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();
            //向收件人地址集合添加邮件地址
            if (email.mailToArray != null)
            {
                for (int i = 0; i < email.mailToArray.Length; i++)
                {
                    myMail.To.Add(email.mailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (email.mailCcArray != null)
            {
                for (int i = 0; i < email.mailCcArray.Length; i++)
                {
                    myMail.CC.Add(email.mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = email.mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = email.mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            myMail.Priority = MailPriority.High;

            myMail.IsBodyHtml = email.isbodyHtml;

            //在有附件的情况下添加附件
            try
            {
                if (email.attachmentsPath != null && email.attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in email.attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(email.mailFrom, email.mailPwd);
            //设置SMTP邮件服务器
            smtp.Host = email.host;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
            }

        }
    }
}