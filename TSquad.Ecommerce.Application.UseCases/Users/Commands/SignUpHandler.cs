using AutoMapper;
using MediatR;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.CrossCutting.Logging;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.UseCases.Users.Commands;

public class SignUpHandler : IRequestHandler<SignUpCommand, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppLogger<AuthApplication> _logger;

    public SignUpHandler(IJwtService jwtService, IUnitOfWork unitOfWork, IMapper mapper,
        IAppLogger<AuthApplication> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Response<bool>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<bool>();
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            response.Message = "User with that email already exists.";
            _logger.LogError("User with that email already exists.", response.Message);
            return response;
        }
            
        var user = _mapper.Map<User>(request);
        response.Data = await _unitOfWork.Users.InsertAsync(user, request.Password);
        if (!response.Data)
        {
            response.Message = "User creation failed.";
            return response;
        }
        
        response.Message = "User created successfully.";
        response.IsSuccess =  true;
        return  response;
    }
}