using Commons.Helper;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using WebSiteAPI.Models;
using WebSiteAPI.Services.Interfaces;

namespace WebSiteAPI.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly EmailConfiguration _emailconfiguration;

        public NotificationService(EmailConfiguration emailconfiguration)
        {
            _emailconfiguration = emailconfiguration;
        }
        public async Task SendEmailAsync(Message message)
        {
            var mail = CreateMessage(message);
            await SendAsync(mail);
        }

        private MimeMessage CreateMessage(Message message)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_emailconfiguration.DisplayName, _emailconfiguration.From));
            mail.To.AddRange(message.To);
            mail.Subject = message.Subject;
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"{message.Content}" };

            return mail;
        }

        private async Task SendAsync(MimeMessage mail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailconfiguration.SmtpServer, _emailconfiguration.Port, true);
                   client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailconfiguration.UserName, _emailconfiguration.Password);
                    await client.SendAsync(mail);
                }catch(DbException dbexc)
                {
                    throw new Exception(dbexc.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
