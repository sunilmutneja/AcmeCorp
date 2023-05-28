using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    internal class CustomerService : ICustomerService
    {
        public Task<Customer> AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(int CustomerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomer(int CustomerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
