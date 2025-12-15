using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.CrossCutting.Logging;

namespace TSquad.Ecommerce.Application.UseCases.Users.Queries;

public class SignInHandler : IRequestHandler<SignInQuery, Response<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IAppLogger<AuthApplication> _logger;

    public SignInHandler(IJwtService jwtService, IUnitOfWork unitOfWork,
        IAppLogger<AuthApplication> logger)
    {
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<Response<TokenDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<TokenDto>();
        
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (user is null)
        {
            response.Message = "User not found.";
            _logger.LogError("User not found.", response.Message);
            return response;
        }
            
        var isValidPassword = await _unitOfWork.Users.CheckPassword(user, request.Password);
        if (!isValidPassword)
        {
            response.Message = "Credenciales invalidas";
            response.IsSuccess = false;
            return response;
        }
            
        var token = _jwtService.GenerateToken(user);
        response.Data = new TokenDto()
        {
            AccessToken = token,
            ExpiresIn = 3600,
            TokenType = "Bearer"
        };
            
        response.IsSuccess = true;
        response.Message = "Token created successfully.";
        return response;
    }
}