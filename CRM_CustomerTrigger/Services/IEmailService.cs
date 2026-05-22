using CRM_CustomerTrigger.Data.Entities;

namespace CRM_Function.Services
{
    public interface IEmailService
    {
        // Sends an email asynchronously to a customer.
        // Task is used to indicate this is an asynchronous operation
        Task SendEmailAsync(Customer customer);
    }
}
