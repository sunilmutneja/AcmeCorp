using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.Entities.Customer;

namespace Persistence.Repository
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _compRepo;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository CustomerRepository, IMapper mapper)
        {
            this._compRepo = CustomerRepository;
            this._mapper = mapper;
        }
        public async Task<ServiceResponse<CustomerDTO>> AddCustomerAsync(createCustomerDTO customerInsertDTO)
        {
            ServiceResponse<CustomerDTO> _response = new();
            try
            {
                if (await _compRepo.CustomerExistAsync(customerInsertDTO.Name))
                {
                    _response.Message = "Exist";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;
                }

                Customer _newCustomer = new()
                {
                    Name= customerInsertDTO.Name,
                    Mobile = customerInsertDTO.Email,
                    CreatedDate = DateTime.UtcNow,
                    Email = customerInsertDTO.Email,
                    IsActive = true
                };

                if (!await _compRepo.CreateCustomerAsync(_newCustomer))
                {
                    _response.Error = "RepoError";
                    _response.Success = false;
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Data = _mapper.Map<CustomerDTO>(_newCustomer);
                _response.Message = "Created";

            }

            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

            }
            return _response;
        }
        public async Task<ServiceResponse<string>> DeleteCustomerAsync(int id)
        {
            ServiceResponse<string> _response = new();

            try
            {
                var _existingCustomer = await _compRepo.CustomerExistAsync(id);

                if (_existingCustomer == false)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;

                }

                if (!await _compRepo.DeleteCustomerAsync(id))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Deleted";

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
        public async Task<ServiceResponse<CustomerDTO>> GetByGUIDAsync(int CustomerGUID)
        {
            ServiceResponse<CustomerDTO> _response = new();

            try
            {

                var _Customer = await _compRepo.GetCustomerByIDAsync(CustomerGUID);

                if (_Customer == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _CustomerDTO = _mapper.Map<CustomerDTO>(_Customer);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _CustomerDTO;


            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }
        public async Task<ServiceResponse<CustomerDTO>> GetByIdAsync(int Id)
        {
            ServiceResponse<CustomerDTO> _response = new();

            try
            {
                var _Customer = await _compRepo.GetCustomerByIDAsync(Id);

                if (_Customer == null)
                {

                    _response.Success = false;
                    _response.Message = "Not Found";
                    return _response;
                }

                var _CustomerDTO = _mapper.Map<CustomerDTO>(_Customer);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _CustomerDTO;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }
        public async Task<ServiceResponse<List<CustomerDTO>>> GetCustomersAsync()
        {
            ServiceResponse<List<CustomerDTO>> _response = new();

            try
            {
                var CustomersList = await _compRepo.GetAllCustomersAsync();

                var CustomerListDto = new List<CustomerDTO>();

                foreach (var item in CustomersList)
                {
                    CustomerListDto.Add(_mapper.Map<CustomerDTO>(item));
                }

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = CustomerListDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }
        public async Task<ServiceResponse<CustomerDTO>> UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO)
        {
            ServiceResponse<CustomerDTO> _response = new();

            try
            {
                var _existingCustomer = await _compRepo.GetCustomerByIDAsync(1);

                if (_existingCustomer == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _existingCustomer.Name = updateCustomerDTO.Name;
                _existingCustomer.Mobile = updateCustomerDTO.Mobile;
                _existingCustomer.Email = updateCustomerDTO.Email;

                if (!await _compRepo.UpdateCustomerAsync(_existingCustomer))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                var _CustomerDTO = _mapper.Map<CustomerDTO>(_existingCustomer);
                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _CustomerDTO;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
    }
}