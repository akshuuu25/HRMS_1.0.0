using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;


using MimeKit.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace HRMS.Email
{
    public class EmailHelper
    {
        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            using (MailMessage mm = new MailMessage("coreweb25@gmail.com", userEmail))
            {
             
                mm.Body = link;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("coreweb25@gmail.com", "Coreweb@257");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                return true;
            }
            // var host = "smtp.gmail.com";
            // var port = 465;

            // var message = new MimeMessage();

            // message.From.Add(new MailboxAddress("Akshesha Desai",userEmail));
            // message.To.Add(new MailboxAddress("Akshesha Desai", userEmail));
            // message.Subject = "Test subject";

            // var bodyBuilder = new BodyBuilder();
            // bodyBuilder.HtmlBody = link;
            // message.Body = bodyBuilder.ToMessageBody();


            // var client = new SmtpClient();


            //client.Connect(host,port, SecureSocketOptions.Auto);
            //client.SendAsync(message);
            //return true;

        }
    }
}
