using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, Response<IEnumerable<CustomerDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    
    public async Task<Response<IEnumerable<CustomerDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<IEnumerable<CustomerDto>>();
        var customers = await _unitOfWork.Customers.GetAllAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data is null) return response;
        
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }
}