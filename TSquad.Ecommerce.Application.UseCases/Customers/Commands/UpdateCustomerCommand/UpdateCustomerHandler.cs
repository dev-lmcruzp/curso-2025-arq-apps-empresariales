using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Domain.Specifications;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();
        var customer = _mapper.Map<Customer>(request);
        
        var countryInBlackListSpecification = new CountryInBlackListSpecification();
        if (!countryInBlackListSpecification.IsSatisfiedBy(customer))
        {
            response.IsSuccess = false;
            response.Message = $"Los clientes del país {customer.Country} no se pueden actualizar porque se encuentran en lista negra";
            return response;
        }
        
        response.Data = await _unitOfWork.Customers.UpdateAsync(customer);
        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Actualición exitosa";
            return response;
        }
        
        response.Message = $"Cliente {request.CustomerId} no existe!!!";
        return response;
    }
}