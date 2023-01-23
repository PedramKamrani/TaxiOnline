using System.Net;
using System.Net.Mail;

namespace Snap.Core.Senders
{
    public  static class EmailSenders
    {
        public static void Sender(string to, string subject, string body)
        {
            var password = "";
            var myMail = "";
            var mail =new MailMessage();
            var smtpServer = new SmtpClient("");

            mail.From = new MailAddress(myMail,"تاکسی");
            mail.To.Add(to);
            mail.Subject=subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            smtpServer.Port = 0;
            smtpServer.Credentials = new NetworkCredential(myMail,password);
            smtpServer.EnableSsl = false;


            smtpServer.Send(mail);


        }
    }
}
