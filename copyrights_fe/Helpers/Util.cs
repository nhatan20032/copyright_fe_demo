using copyrights_fe.Services;
using lamlt.webservice.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace copyrights_fe.Helpers
{
    public class HelpUtils
    {
        public static bool isViettelMobile(string s)
        {
            return s.StartsWith("096") || s.StartsWith("097")
               || s.StartsWith("098")
               || s.StartsWith("032")
               || s.StartsWith("033")
               || s.StartsWith("034")
               || s.StartsWith("035")
               || s.StartsWith("036")
                || s.StartsWith("037")
                 || s.StartsWith("038")
                 || s.StartsWith("086");
        }
        public static bool SendEmail(string email, string title, string body, string fileName)
        {
            web_configService myconfig = new web_configService();
            string lalatv_gmail_user = myconfig.GetBykey("lalatv_gmail_user");
            string lalatv_gmail_pass = myconfig.GetBykey("lalatv_gmail_pass");
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(lalatv_gmail_user);
                mail.To.Add(email);
                mail.Subject = title;
                mail.Body = body;
                mail.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(fileName))
                {
                    mail.Attachments.Add(new Attachment(fileName));
                }
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(lalatv_gmail_user, lalatv_gmail_pass);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }
            }
        }

        internal static string ToMd5(string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
