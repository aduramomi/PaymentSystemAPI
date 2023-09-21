using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.Models;

namespace PaymentSystemAPI.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer?> GetCustomerByCustomerNumber(string CustomerNumber);
        Task<Customer?> GetCustomerById(int id);
        Task<Customer?> GetCustomer(int id, string CustomerNumberToBeUpdated);
        Task CreateCustomer(CreateCustomerDto model);
        Task UpdateCustomer(int id, UpdateCustomerDto model);
        Task DeleteCustomer(int id);
    }
}
