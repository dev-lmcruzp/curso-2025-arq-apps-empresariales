using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using TSquad.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using TSquad.Ecommerce.Application.UseCases.Users.Commands;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Domain.Events;

namespace TSquad.Ecommerce.Application.UseCases.Commons;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
        
        CreateMap<User, SignUpDto>();
        CreateMap<SignUpCommand, User>();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Discount, DiscountDto>().ReverseMap();
        CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();
    }
}