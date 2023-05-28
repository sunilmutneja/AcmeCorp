using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Customer;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICustomerService _customerService;
        private IValidator<CustomerInsertDTO> _validator;
        private IApplicationDbContext @object;
        private readonly IMapper _mapper;


        public CustomerController(IApplicationDbContext dbContext, IValidator<CustomerInsertDTO> validator, IMapper mapper, ICustomerService customerService)
        {
            _validator = validator;
            _dbContext = dbContext;
            _mapper = mapper;
            _customerService = customerService;
        }

        public CustomerController(IApplicationDbContext @object)
        {
            this.@object = @object;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var product = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerInsertDTO customerInsertDTO)
        {
            var customer = await  _customerService.AddCustomer(customerInsertDTO);

            if (customer != null)
            {                
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Customer customer)
        {
            if (customer == null || customer.Id == 0)
                return BadRequest();

            var product = await _dbContext.Customers.FindAsync(customer.Id);

            if (product == null) 
                return NotFound();

            product.Name = customer.Name;
            product.Mobile = customer.Mobile;
            product.Email = customer.Email;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _dbContext.Customers.FindAsync(id);
            if (product == null)
                return NotFound();
            _dbContext.Customers.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
