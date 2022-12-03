
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Backend.Data;

public class EmailSenderService : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;


   



        public EmailSenderService( IOptions<SmtpSettings> smtpSettings)
        {
       
            
            _smtpSettings = smtpSettings.Value;
        }


        public async Task<string> SendEmailAsync(string recipientEmail, string recipientFirstName, string Subject, string Body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));
            message.Subject = Subject;
            message.Body = new TextPart("html")
            {
                Text = $"{Body}"

            };

            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }

   


}


