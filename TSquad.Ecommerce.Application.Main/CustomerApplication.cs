using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Domain.Interface;

namespace TSquad.Ecommerce.Application.Main;

public class CustomerApplication : ICustomerApplication
{
    private readonly ICustomerDomain _customerDomain;
    private readonly IMapper _mapper;

    public CustomerApplication(ICustomerDomain customerDomain, IMapper mapper)
    {
        _customerDomain = customerDomain;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CustomerDto>>();
        try
        {
            var customers = await _customerDomain.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<IEnumerable<CustomerDto>>();
        try
        {
            var customers = await _customerDomain.GetAllWithPaginationAsync(pageNumber, pageSize);
            var count = await _customerDomain.CountAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            if (response.Data is not null)
            {
                response.PageNumber = pageNumber;
                response.PageSize = pageSize;
                // response.TotalPage =  (int)Math.Ceiling((double)count / pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<Response<CustomerDto>> GetAsync(string customerId)
    {
        var response = new Response<CustomerDto>();
        try
        {
            var customer = await _customerDomain.GetAsync(customerId);
            response.Data = _mapper.Map<CustomerDto>(customer);
            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Cliente {customerId} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<Response<bool>> InsertAsync(CustomerDto customerDto)
    {
        var response = new Response<bool>();
        try
        {
            var customer = _mapper.Map<Customer>(customerDto);
            response.Data = await _customerDomain.InsertAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registration successful";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(CustomerDto customerDto)
    {
        var response = new Response<bool>();
        try
        {
            var customer = _mapper.Map<Customer>(customerDto);
            response.Data = await _customerDomain.UpdateAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualición exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Cliente {customer.CustomerId} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(string customerId)
    {
        var response = new Response<bool>();
        try
        {
            response.Data = await _customerDomain.DeleteAsync(customerId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Cliente {customerId} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
}