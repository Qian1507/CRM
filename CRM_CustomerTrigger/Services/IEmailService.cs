using CRM_CustomerTrigger.Data.Entities;

namespace CRM_Function.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Customer customer);
    }
}
