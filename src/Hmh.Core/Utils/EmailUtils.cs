using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hmh.Core.Utils
{
    /// <summary>
    /// 电子邮件 虚类
    /// </summary>
    public abstract class EMail
    {
        public EMail()
        {
            this.From = "724446029@qq.com";
        }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body{get;set;}
        /// <summary>
        /// 收件地址
        /// </summary>
        public string To{ get;set;}
        /// <summary>
        /// 发件人,默认为系统邮箱
        /// </summary>
        public string From{get;set;}
        /// <summary>
        /// 发送
        /// </summary>
        public abstract void Send();
        /// <summary>
        /// 群发邮件
        /// </summary>
        public abstract void GroupSend(string[] groupTo);
        /// <summary>
        /// 创建一个邮件实体
        /// </summary>
        /// <returns></returns>
        public static EMail CreateInstance()
        {
            return new SmtpMail();
        }
    }

    /// <summary>
    /// SMTP邮件
    /// </summary>
    public class SmtpMail : EMail
    {
        private SmtpClient smtpClient;
        public SmtpMail()
        {
            smtpClient = new SmtpClient();
            smtpClient.Timeout = 100000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = "smtp.qq.com";//指定SMTP服务器
            smtpClient.Port = 25;
            smtpClient.Credentials = new System.Net.NetworkCredential("724446029", "198612xia");//用户名和密码
            smtpClient.EnableSsl = true;
            if (smtpClient.Port != 25)
            {//gmail:587
                smtpClient.EnableSsl = true;
            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        public override void Send()
        {
            MailMessage mailMessage = PreSend();
            mailMessage.To.Add(new MailAddress(To));
            SendOut(mailMessage);
        }

        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="groupTo"></param>
        public override void GroupSend(string[] groupTo)
        {
            MailMessage mailMessage = PreSend();
            foreach (string to in groupTo)
            {
                mailMessage.To.Add(new MailAddress(to));
            }
            SendOut(mailMessage);
        }


        /// <summary>
        /// 准备MailMessage类
        /// </summary>
        /// <returns></returns>
        private MailMessage PreSend()
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(From);
            mailMessage.Subject = this.Subject;
            mailMessage.Body = this.Body;//内容
            mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            mailMessage.SubjectEncoding = System.Text.Encoding.Default;
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Normal;//优先级
            return mailMessage;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mailMessage"></param>
        private void SendOut(MailMessage mailMessage)
        {
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
                //To、CC 和 BCC 中没有收件人。
            }
            catch (ObjectDisposedException e)
            {
                throw e;
                // 此对象已被释放。
            }
            catch (InvalidOperationException e)
            {
                throw e;
                //此 SmtpClient 有一个 SendAsync 调用正在进行。- 或 - 
                //Host 为 空引用（在 Visual Basic 中为 Nothing）。- 或 -
                //Host 是空字符串 ("")。或者 Port 是零。
            }
            catch (SmtpFailedRecipientsException e)
            {
                throw e;
                //message 无法传递给 To、CC 或 BCC 中的一个或多个收件人。 
            }
            catch (SmtpException e)
            {
                throw e;
                //连接到 SMTP 服务器失败。- 或 -身份验证失败。- 或 -操作超时。
            }
        }
    }
}
