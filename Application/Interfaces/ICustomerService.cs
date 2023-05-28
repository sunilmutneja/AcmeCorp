using Domain.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int CustomerId);
        Task<Customer> AddCustomer(CustomerInsertDTO customerInsertDTO);
        Task<Customer> UpdateCustomer(Customer customer);
        Task DeleteCustomer(int CustomerId);
    }
}
