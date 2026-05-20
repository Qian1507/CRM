using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Function
{
    public class EmailService:IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;  
        }


        public async Task SendEmailAsync(string toEmail, string sellerName, string subject, string body)
        {
            var client = new SendGridClient(apiKey);

            var message = new SendGridMessage
            {
                From = new EmailAddress(fromEmail, "CRM System"),
                Subject = subject,
                PlainTextContent = body
            };

            message.AddTo(new EmailAddress(toEmail, sellerName));

            await client.SendEmailAsync(message);
        }
    }
}
