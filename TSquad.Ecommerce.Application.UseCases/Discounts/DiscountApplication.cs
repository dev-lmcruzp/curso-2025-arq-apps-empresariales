using System.Text.Json;
using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Infrastructure;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.Application.Validator;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Domain.Events;

namespace TSquad.Ecommerce.Application.UseCases.Discounts;

public class DiscountApplication : IDiscountApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly DiscountDtoValidator _discountDtoValidator;
    private readonly IEventBus _eventBus;
    private readonly ISendmail _sendmail;


    public DiscountApplication(IUnitOfWork unitOfWork, IMapper mapper,  
        DiscountDtoValidator discountDtoValidator, IEventBus eventBus, ISendmail sendmail
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _discountDtoValidator = discountDtoValidator;
        _eventBus = eventBus;
        _sendmail = sendmail;
    }

    public async Task<ResponsePagination<List<DiscountDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        var response = new ResponsePagination<List<DiscountDto>>();
        try
        {
            var customers = await _unitOfWork.Discounts.GetAllWithPaginationAsync(pageNumber, pageSize);
            var count = await _unitOfWork.Discounts.CountAsync();
            response.Data = _mapper.Map<List<DiscountDto>>(customers);
            if (response.Data is not null)
            {
                response.PageNumber = pageNumber;
                response.PageSize = pageSize;
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

    public async Task<Response<List<DiscountDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = new Response<List<DiscountDto>>();
        try
        {
            var discounts = await _unitOfWork.Discounts.GetAllAsync(cancellationToken);
            response.Data = _mapper.Map<List<DiscountDto>>(discounts);
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

    public async Task<Response<DiscountDto?>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = new Response<DiscountDto?>();
        try
        {
            var customer = await _unitOfWork.Discounts.GetAsync(id, cancellationToken);
            response.Data = _mapper.Map<DiscountDto>(customer);
            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Cliente {id} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<Response<bool>> InsertAsync(DiscountDto discountDto, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();
        try
        {
            var validation = await _discountDtoValidator.ValidateAsync(discountDto, cancellationToken);
            
            if (!validation.IsValid)
            {
                response.Message = "Errores de validación";
                response.Errors = validation.Errors;
                return response;
            }
            
            var discount = _mapper.Map<Discount>(discountDto);
            await _unitOfWork.Discounts.InsertAsync(discount);
            response.Data = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registration successful";
                var discountCreateEvent = _mapper.Map<DiscountCreatedEvent>(discount); 
                _eventBus.Publish(discountCreateEvent);
                
                /*Enviar correo*/
                 await _sendmail.SendEmailAsync(response.Message, JsonSerializer.Serialize(discount), cancellationToken);
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> UpdateAsync(DiscountDto discountDto, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();
        try
        {
            var discount = _mapper.Map<Discount>(discountDto);
            await _unitOfWork.Discounts.UpdateAsync(discount);
            response.Data = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualición exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Descuento {discountDto.Id} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = new Response<bool>();
        try
        {
            await _unitOfWork.Discounts.DeleteAsync(id.ToString());
            response.Data = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación exitosa";
                return response;
            }
            
            response.IsSuccess = true;
            response.Message = $"Descuento {id} no existe!!!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }

        return response;
    }
}