using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;
        private IValidator<CustomerInsertDTO> _validator;
        private readonly IMapper _mapper;
        /// <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        public CustomerController(IApplicationDbContext dbContext, IValidator<CustomerInsertDTO> validator, IMapper mapper)
        {
            _validator = validator;
            _dbContext = dbContext;
            _mapper = mapper;
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
            var result = await _validator.ValidateAsync(customerInsertDTO);

            if (result.IsValid)
            {
               Customer customer = _mapper.Map<Customer>(customerInsertDTO);               

                customer.CreatedDate =  DateTime.Now;
                customer.ModifiedDate = DateTime.Now;
                customer.IsActive = true;

                _dbContext.Customers.Add(customer);
                await _dbContext.SaveChangesAsync();
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
