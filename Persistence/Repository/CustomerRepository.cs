using Application.Interfaces;
using Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _dataContext;

    public CustomerRepository(IApplicationDbContext dataContext)
    {
        this._dataContext = dataContext;
    }
    public async Task<bool> CustomerExistAsync(string customerName)
    {
        return await _dataContext.Customers.AnyAsync(Comp => Comp.Name== customerName);
    }

    public async Task<bool> CustomerExistAsync(int id)
    {
        return await _dataContext.Customers.AnyAsync(Comp => Comp.Id == id);
    }

    public async Task<bool> CreateCustomerAsync(Customer Customer)
    {
        await _dataContext.Customers.AddAsync(Customer);
        return await Save();
    }
    public async Task<bool> UpdateCustomerAsync(Customer Customer)
    {
        _dataContext.Customers.Update(Customer);
        return await Save();
    }

    public async Task<ICollection<Customer>> GetAllCustomersAsync()
    {
        return await _dataContext.Customers.ToListAsync();
    }

    public async Task<Customer> GetCustomerByIDAsync(int customerId)
    {
        return await _dataContext.Customers.FirstOrDefaultAsync(Comp => Comp.Id == customerId);
    }   

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var _exisitngCustomer = await GetCustomerByIDAsync(id);

        if (_exisitngCustomer != null)
        {
            _dataContext.Customers.Remove(_exisitngCustomer);
            return await Save();
        }
        return false;
    }

    private async Task<bool> Save()
    {
        return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
    }
}