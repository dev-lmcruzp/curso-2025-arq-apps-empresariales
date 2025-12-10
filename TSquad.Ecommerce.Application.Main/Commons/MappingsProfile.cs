using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.Application.Main.Commons;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<User, SignUpDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}