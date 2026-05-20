using CRM.Api.Entities;

namespace CRM_Api.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(string id);
        Task<Customer?> UpdateAsync(string id, Customer customer);
        Task<bool> DeleteAsync(string id);

        Task<IEnumerable<Customer>> SearchAsync(string? name, string? sellerName);
    }
}
