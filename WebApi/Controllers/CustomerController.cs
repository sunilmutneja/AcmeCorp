using Application.Interfaces;
using Domain.Entities.Customer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _customerService.GetCustomersAsync();

            return Ok(companies);
        }

        [HttpGet("{CustomerID:int}", Name = "GetByCustomerID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CustomerDTO>> GetCustomerID(int CustomerID)
        {
            if (CustomerID <= 0)
            {
                return BadRequest(CustomerID);
            }
            var CustomerFound = await _customerService.GetByIdAsync(CustomerID);

            if (CustomerFound.Data == null)
            {
                return NotFound();
            }

            return Ok(CustomerFound);
        }    

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] createCustomerDTO createCustomerDTO)
        {
            if (createCustomerDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newCustomer = await _customerService.AddCustomerAsync(createCustomerDTO);

            if (_newCustomer.Success == false && _newCustomer.Message == "Exist")
            {
                return Ok(_newCustomer);
            }

            if (_newCustomer.Success == false && _newCustomer.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding Customer {createCustomerDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newCustomer.Success == false && _newCustomer.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding Customer {createCustomerDTO}");
                return StatusCode(500, ModelState);
            }

            return Ok(_newCustomer);

        }

        [HttpPatch("{customerId:int}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            if (updateCustomerDTO == null)
            {
                return BadRequest(ModelState);
            }

            var _updateCustomer = await _customerService.UpdateCustomerAsync(updateCustomerDTO);

            if (_updateCustomer.Success == false && _updateCustomer.Message == "NotFound")
            {
                return Ok(_updateCustomer);
            }

            if (_updateCustomer.Success == false && _updateCustomer.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating Customer {updateCustomerDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateCustomer.Success == false && _updateCustomer.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when updating Customer {updateCustomerDTO}");
                return StatusCode(500, ModelState);
            }


            return Ok(_updateCustomer);
        }

        [HttpDelete("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {

            var _deleteCustomer = await _customerService.DeleteCustomerAsync(customerId);


            if (_deleteCustomer.Success == false && _deleteCustomer.Message == "NotFound")
            {
                ModelState.AddModelError("", "Customer Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteCustomer.Success == false && _deleteCustomer.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting Customer");
                return StatusCode(500, ModelState);
            }

            if (_deleteCustomer.Success == false && _deleteCustomer.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting Customer");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}


