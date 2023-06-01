using Domain.Common;
using Domain.Entities.Customer;

namespace Application.Interfaces
{
    public interface ICustomerService
    { 
        Task<ServiceResponse<List<CustomerDTO>>> GetCustomersAsync();
 
        Task<ServiceResponse<CustomerDTO>> GetByIdAsync(int Id);

        Task<ServiceResponse<CustomerDTO>> AddCustomerAsync(createCustomerDTO customerInsertDTO);

        Task<ServiceResponse<CustomerDTO>> UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO);

        Task<ServiceResponse<string>> DeleteCustomerAsync(int id);

    }
}
