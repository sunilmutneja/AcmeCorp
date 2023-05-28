using AutoMapper;

namespace Domain.Entities
{
    public class CustomerProfile : Profile
    {
       public CustomerProfile()
        {
            CreateMap<CustomerInsertDTO, Customer>();
           
        }
    }
}
