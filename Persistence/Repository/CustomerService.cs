using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Customer;
using FluentValidation;

namespace Persistence.Repository
{
    public class CustomerService : ICustomerService
    {
        private readonly IApplicationDbContext _dbContext;
        private IValidator<CustomerInsertDTO> _validator;
        private readonly IMapper _mapper;

        public CustomerService(IApplicationDbContext dbContext, IValidator<CustomerInsertDTO> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Customer> AddCustomer(CustomerInsertDTO customerInsertDTO)
        {
            Customer customer = null;

            try
            {
                var result = await _validator.ValidateAsync(customerInsertDTO);

                if (result.IsValid)
                {
                    customer = _mapper.Map<Customer>(customerInsertDTO);

                    customer.CreatedDate = DateTime.Now;
                    customer.ModifiedDate = DateTime.Now;
                    customer.IsActive = true;

                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();                   
                }

                return customer;
            }

            catch(Exception)
            {
                throw;
            }
                        
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

        public Task<Customer> AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
