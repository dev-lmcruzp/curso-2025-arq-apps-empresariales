using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();
        var customer = _mapper.Map<Customer>(request);
        response.Data = await _unitOfWork.Customers.InsertAsync(customer);
        if (!response.Data)
        {
            response.Message = "Registration unsuccessful";
            return response;
        }
        
        response.IsSuccess = true;
        response.Message = "Registration successful";
        return  response;
    }
}