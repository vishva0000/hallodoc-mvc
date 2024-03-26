using MimeKit;

namespace BusinessLayer.Utility
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
            }
            return Task.CompletedTask;
        }

        //public Task SendEmailAsync(string email, string subject, string message, List<string>? files)
        //{
        //    var emailToSend = new MimeMessage();
        //    emailToSend.From.Add(MailboxAddress.Parse("vishva.rami@etatvasoft.com"));
        //    emailToSend.To.Add(MailboxAddress.Parse(email));
        //    emailToSend.Subject = subject;
        //    BodyBuilder builder = new BodyBuilder();
        //    builder.TextBody = message;
        //    string path = "D:\\postgreconnection\\HalloDoc\\wwwroot\\uploads";
        //    foreach(string item in files)
        //    {
        //        string fullpath = Path.Combine(path, item);
        //        builder.Attachments.Add(fullpath);
        //    }
        //    emailToSend.Body = builder.ToMessageBody();
        //    //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };


        //    using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
        //    {
        //        emailClient.Connect("mail.etatvasoft.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        //        emailClient.Authenticate("vishva.rami@etatvasoft.com", "z,x061v4m97h");
        //        emailClient.Send(emailToSend);
        //        emailClient.Disconnect(true);
        //    }
        //    return Task.CompletedTask;
        //}

        public Task SendFileAsync(string email, string subject, string message, List<string>? files)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("vishva.rami@etatvasoft.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = message;
            string path = "D:\\postgreconnection\\HalloDoc\\wwwroot\\uploads\\";
            foreach (string item in files)
            {
                if(item != null)
                {
                    string fullpath = Path.Combine(path, item.Trim());
                    builder.Attachments.Add(fullpath);
                }
               
            }
            emailToSend.Body = builder.ToMessageBody();
            //emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };


            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect("mail.etatvasoft.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("vishva.rami@etatvasoft.com", "z,x061v4m97h");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}



