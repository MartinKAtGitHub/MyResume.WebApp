using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio_Website_Core.Utilities.MailService
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration configuration;

        private string _fromName = "My Resume";
        private string _fromEmailAddress = "martinwebsitemail@gmail.com";

        public MessageService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string toName, string toEmailAdress, string subject, string message)
        {
            var body = new BodyBuilder()
            {
                HtmlBody = message
            };

            MimeMessage msg = new MimeMessage();
            
            msg.From.Add(new MailboxAddress(_fromName, _fromEmailAddress));
            msg.To.Add(new MailboxAddress(toName, toEmailAdress));
            msg.Subject = subject;
            msg.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // Note: since GMail requires SSL at connection time, use the "smtps"
                // protocol instead of "smtp".
                var uri = new Uri("smtps://smtp.gmail.com:465");

                await client.ConnectAsync(uri);
                await client.AuthenticateAsync(configuration.GetSection("Email")["EmailAccount"], configuration.GetSection("Email")["EmailPw"]);
                await client.SendAsync(msg);
                await client.DisconnectAsync(true);
            }

        }
    }
}
