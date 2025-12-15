using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;

public class GetAllWithPaginationCustomerHandler : IRequestHandler<GetAllWithPaginationCustomerQuery, ResponsePagination<IEnumerable<CustomerDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllWithPaginationCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ResponsePagination<IEnumerable<CustomerDto>>> Handle(GetAllWithPaginationCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDto>>();
        var customers = await _unitOfWork.Customers.GetAllWithPaginationAsync(request.PageNumber, request.PageSize);
        var count = await _unitOfWork.Customers.CountAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data is null) return response;
        
        response.PageNumber = request.PageNumber;
        response.PageSize = request.PageSize;
        response.TotalCount = count;
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }
}