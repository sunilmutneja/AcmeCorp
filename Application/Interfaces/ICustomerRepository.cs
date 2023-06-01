using Domain.Entities.Customer;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {      
        Task<ICollection<Customer>> GetAllCustomersAsync();  
        Task<Customer> GetCustomerByIDAsync(int CustomerId);      
        Task<bool> CustomerExistAsync(string CustomerName);     
        Task<bool> CustomerExistAsync(int Id);      
        Task<bool> CreateCustomerAsync(Customer Customer);      
        Task<bool> UpdateCustomerAsync(Customer Customer);
        Task<bool> DeleteCustomerAsync(int id);
    }

}
