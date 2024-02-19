using Castle.Core.Smtp;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("vishva.rami@etatvasoft.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };


            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect("mail.etatvasoft.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("vishva.rami@etatvasoft.com", "z,x061v4m97h");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
                return Task.CompletedTask;
            }
        }
    }
}



