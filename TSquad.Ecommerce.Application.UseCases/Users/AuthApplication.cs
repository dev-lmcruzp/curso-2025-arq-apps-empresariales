using AutoMapper;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.CrossCutting.Logging;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.UseCases.Users;

public class AuthApplication : IAuthApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<AuthApplication> _logger;

    public AuthApplication(IJwtService jwtService, IUnitOfWork unitOfWork, IMapper mapper,
        IAppLogger<AuthApplication> logger)
    {
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<bool>> SignUpAsync(SignUpDto signUpDto)
    {
        var response = new Response<bool>();
        try
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(signUpDto.Email);
            if (existingUser is not null)
            {
                response.Message = "User with that email already exists.";
                _logger.LogError("User with that email already exists.", response.Message);
                return response;
            }
            
            var user = _mapper.Map<User>(signUpDto);
            response.Data = await _unitOfWork.Users.InsertAsync(user, signUpDto.Password);
            if (response.Data)
            {
                response.Message = "User created successfully.";
                response.IsSuccess =  true;
                return  response;
            }
            
            response.Message = "User creation failed.";
            
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<Response<TokenDto>> SignInAsync(SignInDto signInDto)
    {
        var response = new Response<TokenDto>();
        try
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(signInDto.Email);
            if (user is null)
            {
                response.Message = "User not found.";
                _logger.LogError("User not found.", response.Message);
                return response;
            }
            
            var isValidPassword = await _unitOfWork.Users.CheckPassword(user, signInDto.Password);
            if (!isValidPassword)
            {
                response.Message = "Credenciales invalidas";
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

        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }
}