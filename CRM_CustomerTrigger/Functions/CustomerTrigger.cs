using CRM_CustomerTrigger.Data.Entities;
using CRM_Function.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CRM_CustomerTrigger.Functions
{
    public class CustomerTrigger
    {
        private readonly ILogger<CustomerTrigger> _logger;
        private readonly IEmailService emailService;

        public CustomerTrigger(ILogger<CustomerTrigger> logger, IEmailService emailService)
        {
            _logger = logger;
            this.emailService = emailService;
        }

        [Function("CustomerTrigger")]
        public async Task Run([CosmosDBTrigger(
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
                await emailService.SendEmailAsync(customer);
            }
        }
    }
}
