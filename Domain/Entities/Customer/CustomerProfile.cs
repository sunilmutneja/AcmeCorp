using AutoMapper;

namespace Domain.Entities.Customer
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>().ReverseMap();

        }
    }
}
