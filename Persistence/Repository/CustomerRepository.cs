using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public CustomerRepository( ApplicationDbContext dbContext)
        {
             _DbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _DbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int CustomerId)
        {
            return await _DbContext.Customers.FirstOrDefaultAsync(e => e.Id == CustomerId);
        }

        public async Task<Customer> AddCustomer(Customer Customer)
        {
            var result = await _DbContext.Customers.AddAsync(Customer);
            await _DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer?> UpdateCustomer(Customer Customer)
        {
            var result = await _DbContext.Customers.FirstOrDefaultAsync(e => e.Id == Customer.Id);

            if (result != null)
            {
                result.Name = Customer.Name;
                result.Email = Customer.Email;
                result.Mobile = Customer.Mobile;
      
                await _DbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteCustomer(int CustomerId)
        {
            var result = await _DbContext.Customers.FirstOrDefaultAsync(e => e.Id == CustomerId);
            if (result != null)
            {
                _DbContext.Customers.Remove(result);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _DbContext.SaveChangesAsync();
        }
    }
}
