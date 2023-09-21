using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.IServices;
using PaymentSystemAPI.Models;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace PaymentSystemAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private PaymentSystemDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(PaymentSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByCustomerNumber(string CustomerNumber)
        {
            return await GetCustomer(CustomerNumber);
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await GetCustomer(id);
        }

        public async Task<Customer?> GetCustomer(int id, string CustomerNumberToBeUpdated)
        {
            var Customer = await _context.Customers.Where(m => m.Id != id && m.CustomerNumber.ToUpper() == CustomerNumberToBeUpdated.ToUpper()).SingleOrDefaultAsync();

            return Customer;
        }

        public async Task CreateCustomer(CreateCustomerDto model)
        {           
            var Customer = _mapper.Map<Customer>(model);

            await _context.Customers.AddAsync(Customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(int id, UpdateCustomerDto model)
        {
            var Customer = await GetCustomer(id);
           
            _mapper.Map(model, Customer);
            _context.Customers.Update(Customer);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var Customer = await GetCustomer(id);
           
            _context.Customers.Remove(Customer);

            await _context.SaveChangesAsync();
        }

        private async Task<Customer?> GetCustomer(string CustomerNumber)
        {
            if (string.IsNullOrEmpty(CustomerNumber))
                return null;

            var Customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerNumber.ToUpper() == CustomerNumber.ToUpper());

            return Customer;
        }

        private async Task<Customer?> GetCustomer(int id)
        {
            var Customer = await _context.Customers.FindAsync(id);

            return Customer;
        }


    }
}
