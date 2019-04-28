using System;
using System.Net;
using System.Net.Mail;

namespace MM.Helper.Net
{
    /// <summary>
    /// Email帮助类
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        public string Ex       { get; private set; }

        /// <summary>
        /// 是否使用SSL
        /// </summary>
        public bool IsSSL      { get; set; }
        /// <summary>
        /// smtp地址
        /// </summary>
        public string Smtp     { get; set; } = "smtp.qq.com";
        /// <summary>
        /// 邮件昵称
        /// </summary>
        public string Nick     { get; set; }
        /// <summary>
        /// 自己的邮件地址
        /// </summary>
        public string MyEmail  { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Email(){

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myEmail">自己的邮件地址</param>
        /// <param name="nick">邮件昵称</param>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="smtp">smtp地址</param>
        public Email(string myEmail, string username, string password, string nick = null, string smtp = "smtp.qq.com")
        {
            Init(myEmail, username, password, nick, smtp);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="myEmail">自己的邮件地址</param>
        /// <param name="nick">邮件昵称</param>
        /// <param name="username">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <param name="smtp">smtp地址</param>
        public void Init(string myEmail, string username, string password, string nick = null, string smtp = "smtp.qq.com") {
            MyEmail = myEmail;
            Nick = nick;
            Username = username;
            Password = password;
            Smtp = smtp;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="CopyEmail">抄送邮箱</param>
        /// <param name="isBodyHtml">是否Html格式的邮件</param>
        /// <returns>发送成功返回true，失败返回false</returns>
        public bool Send(string email, string title, string content, string CopyEmail = null, bool isBodyHtml = false)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(content))
            {
                return false;
            }
            try
            {
                MailMessage msg = new MailMessage()
                {
                    From = new MailAddress(MyEmail, Nick, System.Text.Encoding.UTF8),//设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                    Subject = title,                                                 //设置邮件标题
                    SubjectEncoding = System.Text.Encoding.UTF8,                     //邮件标题编码
                    Body = content,                                                  //设置邮件内容
                    BodyEncoding = System.Text.Encoding.UTF8,                        //邮件标题编码
                    IsBodyHtml = isBodyHtml                                          //是否是HTML邮件
                };
                msg.To.Add(email);                                                   //设置收件人,可添加多个,添加方法与下面的一样
                if (CopyEmail != null)
                {
                    msg.CC.Add(CopyEmail);                                           //设置抄送人
                }
                SmtpClient client = new SmtpClient(Smtp, 25)
                {
                    Credentials = new NetworkCredential(Username, Password),         //设置发送人的邮箱账号和密码
                    EnableSsl = IsSSL                                                //启用ssl,也就是安全发送
                };                                                                   //设置邮件发送服务器
                client.Send(msg);                                                    //发送邮件
                return true;
            }
            catch (Exception ex)
            {
                Ex = ex.Message;
                return false;
            }
        }
    }
}
