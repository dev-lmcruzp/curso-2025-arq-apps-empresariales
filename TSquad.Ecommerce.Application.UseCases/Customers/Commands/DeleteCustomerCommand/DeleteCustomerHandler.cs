using MediatR;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>
        {
            Data = await _unitOfWork.Customers.DeleteAsync(request.CustomerId)
        };
        
        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Eliminación exitosa";
            return response;
        }
        
        response.Message = $"Cliente {request.CustomerId} no existe!!!";
        return response;
    }
}