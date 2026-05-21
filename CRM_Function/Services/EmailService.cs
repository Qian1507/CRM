using CRM_Function.Data.Entities;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace CRM_Function.Services
{
    public class EmailService:IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _config;

        public EmailService(ILogger<EmailService> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        public async Task SendEmailAsync(Customer customer)
        {
            var username = _config["Mailtrap:Username"];
            var password = _config["Mailtrap:Password"];


            var email = new MimeMessage();

            email.From.Add(
                MailboxAddress.Parse("crm@test.com"));

            email.To.Add(
                MailboxAddress.Parse(customer.Seller.Email));

            email.Subject = "New Customer Assigned";

            email.Body = new TextPart("plain")
            {
                Text =
                        $"""
                    Hello {customer.Seller.Name},

                    You are now responsible for a new customer.

                    Customer Information:

                    Name: {customer.Name}
                    Title: {customer.Title}
                    Phone: {customer.Phone}
                    Email: {customer.Email}
                    Address: {customer.Address}

                    Regards,
                    CRM System
                    """
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                "sandbox.smtp.mailtrap.io",
                2525,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(username,password);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);

            _logger.LogInformation(
                $"Email sent to {customer.Seller.Email}"); 

            
        }
    }
}
