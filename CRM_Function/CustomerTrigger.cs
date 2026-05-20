using System;
using System.Collections.Generic;
using CRM_Function.Data.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CRM_Function
{
    public class CustomerTrigger
    {
        private readonly ILogger<CustomerTrigger> _logger;
        private readonly IEmailService emailService;

        public CustomerTrigger(ILogger<CustomerTrigger> logger,IEmailService emailService)
        {
            _logger = logger;
            this.emailService = emailService;
        }

        [Function("CustomerTrigger")]
        public void Run([CosmosDBTrigger(
            databaseName: "CRMDB",
            containerName: "Customers",
            Connection = "CosmosDBConnection",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Customer>? customers)
        {
            if (customers is null || customers.Count == 0)
            {
                _logger.LogInformation("No customer changes detected.");
                return;
            }

            foreach (var customer in customers)
            {
                if (customer.Seller is null || string.IsNullOrWhiteSpace(customer.Seller.Email))
                {
                    _logger.LogWarning("Customer {CustomerId} has no seller email.", customer.Id);
                    continue;
                }

                var subject = $"Customer assigned: {customer.Name}";

                var body =
                            $"""
                        Hello {customer.Seller.Name},

                        You have been assigned as the responsible seller for the following customer.

                        Customer details:
                        Name: {customer.Name}
                        Title: {customer.Title}
                        Phone: {customer.Phone}
                        Email: {customer.Email}
                        Address: {customer.Address}
                        """;

                await emailService.SendEmailAsync(
                    customer.Seller.Email,
                    customer.Seller.Name,
                    subject,
                    body);

                _logger.LogInformation(
                    "Email sent to {SellerEmail} for customer {CustomerName}",
                    customer.Seller.Email,
                    customer.Name);
            }
        }
    }

   
}
