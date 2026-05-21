using CRM_CustomerTrigger.Data.Entities;
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

        // Inject dependencies for logging and configuration access
        public EmailService(ILogger<EmailService> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        public async Task SendEmailAsync(Customer customer)
        {
            // Read Mailtrap username and password from configuration
            // The configuration values are stored in local.settings.json,
            // which is ignored by Git for security reasons

            var username = _config["MailtrapUsername"];
            var password = _config["MailtrapPassword"];

            // Create a new email message
            var email = new MimeMessage();


            // Set sender email address
            email.From.Add(
                MailboxAddress.Parse("crm@test.com"));

            // Set receiver email address
            email.To.Add(
                MailboxAddress.Parse(customer.Seller.Email));


            // Set email subject
            email.Subject = "New Customer Assigned";


            // Set email body content
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

            // Create SMTP client
            using var smtp = new MailKit.Net.Smtp.SmtpClient();


            // Connect to Mailtrap SMTP server
            await smtp.ConnectAsync(
                "sandbox.smtp.mailtrap.io",
                2525,
                SecureSocketOptions.StartTls);

            // Check if username or password is missing

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Mailtrap credentials missing");
            }

            // Authenticate with Mailtrap
            await smtp.AuthenticateAsync(username,password);

            // Send the email
            await smtp.SendAsync(email);

            // Disconnect from SMTP server
            await smtp.DisconnectAsync(true);

            // Log successful email sending
            _logger.LogInformation(
                $"Email sent to {customer.Seller.Email}"); 

            
        }
    }
}
