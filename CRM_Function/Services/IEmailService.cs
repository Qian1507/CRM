using CRM_Function.Data.Entities;

namespace CRM_Function.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Customer customer);
    }
}
