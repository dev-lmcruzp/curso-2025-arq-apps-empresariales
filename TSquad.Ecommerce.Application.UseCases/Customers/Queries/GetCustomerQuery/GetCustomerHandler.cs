using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Response<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<CustomerDto>();
        var customer = await _unitOfWork.Customers.GetAsync(request.CustomerId);
        response.Data = _mapper.Map<CustomerDto>(customer);
        if (response.Data is null)
        {
            response.Message = $"Cliente {request.CustomerId} no existe!!!";
            return response;
        }
        
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }
}