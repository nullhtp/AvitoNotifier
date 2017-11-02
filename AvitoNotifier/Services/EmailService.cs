using System;
using System.Collections.Generic;
using AvitoNotifier.Extensions;
using AvitoNotifier.Interfaces;
using AvitoNotifier.Preferences;
using MailKit.Net.Smtp;
using MimeKit;
using NLog;

namespace AvitoNotifier
{
    public class EmailService : IEmailNotifier
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        
        private readonly string _server;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _password;
        private readonly int _port;

            public EmailService(string server, string fromEmail, string fromName, string password,
            int port = 25)
        {
            _server = server;
            _fromEmail = fromEmail;
            _fromName = fromName;
            _password = password;
            _port = port;
        }

        private async void SendAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_fromName, _fromEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_server, _port, false);
                    await client.AuthenticateAsync(_fromEmail, _password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
            }
        }

        public void Notify(NotificationRecord target, IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                var message = target.Template.TemplateReplace(item);
                SendAsync(target.Value,item.Title,message);
            }
        }
    }
}