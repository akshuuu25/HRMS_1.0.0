using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using MailKit.Net.Smtp;
using MimeKit.Text;
using System.Threading.Tasks;




namespace HRMS.Email
{
    public class EmailHelper
    {
        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            var host = "smtp.gmail.com";
            var port = 465;

            var message = new MimeMessage();
           
            message.From.Add(new MailboxAddress("Akshesha Desai",userEmail));
            message.To.Add(new MailboxAddress("Akshesha Desai", userEmail));
            message.Subject = "Test subject";


            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = link;
            message.Body = bodyBuilder.ToMessageBody();


            var client = new SmtpClient();


            client.Connect(host, port, SecureSocketOptions.Auto);
            client.SendAsync(message);
            return true;

            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("akshesha25@gmail.com");
            //mailMessage.To.Add(new MailAddress(userEmail));

            //mailMessage.Subject = "Password Reset";
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Body = link;

            //SmtpClient client = new SmtpClient();
            //client.Credentials = new System.Net.NetworkCredential("akshesha25@gmail.com", "Aksh@desai25");
            //client.Host = "smtpout.secureserver.net";
            //client.Port = 465;
            //client.EnableSsl = true;

            //try
            //{
            //    client.SendMailAsync(mailMessage);
            //    return true;
            //}
            //catch(Exception ex) 
            //{
            //    return false;
            //}
            // return false;
        }
    }
}
