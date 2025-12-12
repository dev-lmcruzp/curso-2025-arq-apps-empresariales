using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.UseCases.Customers;

public class CustomerApplication : ICustomerApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerApplication(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CustomerDto>>();
        var customers = await _unitOfWork.Customers.GetAllAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data is null) return response;
        
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }

    public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDto>>();
        var customers = await _unitOfWork.Customers.GetAllWithPaginationAsync(pageNumber, pageSize);
        var count = await _unitOfWork.Customers.CountAsync();
        response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        if (response.Data is null) return response;
        
        response.PageNumber = pageNumber;
        response.PageSize = pageSize;
        response.TotalCount = count;
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }

    public async Task<Response<CustomerDto>> GetAsync(string customerId)
    {
        var response = new Response<CustomerDto>();
        var customer = await _unitOfWork.Customers.GetAsync(customerId);
        response.Data = _mapper.Map<CustomerDto>(customer);
        if (response.Data is null)
        {
            response.Message = $"Cliente {customerId} no existe!!!";
            return response;
        }
        
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        return response;
    }

    public async Task<Response<bool>> InsertAsync(CustomerDto customerDto)
    {
        var response = new Response<bool>();
        var customer = _mapper.Map<Customer>(customerDto);
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

    public async Task<Response<bool>> UpdateAsync(CustomerDto customerDto)
    {
        var response = new Response<bool>();
        var customer = _mapper.Map<Customer>(customerDto);
        response.Data = await _unitOfWork.Customers.UpdateAsync(customer);
        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Actualición exitosa";
            return response;
        }
        
        response.Message = $"Cliente {customer.CustomerId} no existe!!!";
        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        var response = new Response<bool>
        {
            Data = await _unitOfWork.Customers.DeleteAsync(customerId)
        };
        
        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = "Eliminación exitosa";
            return response;
        }
        
        response.Message = $"Cliente {customerId} no existe!!!";
        return response;
    }
}