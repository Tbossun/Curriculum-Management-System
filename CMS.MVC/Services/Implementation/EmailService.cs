﻿using CMS.Data.Entities;
using CMS.MVC.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace CMS.MVC.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IConfiguration _configuration;

        public EmailService(EmailConfiguration emailConfiguration, IConfiguration configuration)
        {
            _emailConfiguration = emailConfiguration;
            _configuration = configuration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("CMS", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Add null check for message.Content
            if (!string.IsNullOrWhiteSpace(message.Content))
            {
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            }

            return emailMessage;
        }
        public void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
