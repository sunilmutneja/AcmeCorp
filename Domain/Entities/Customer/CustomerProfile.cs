using AutoMapper;

namespace Domain.Entities.Customer
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerInsertDTO, Customer>();

        }
    }
}
