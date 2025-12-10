using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Interface;

public interface IAuthApplication
{
    Task<Response<bool>> SignUpAsync(SignUpDto signUpDto);
    Task<Response<TokenDto>> SignInAsync(SignInDto signInDto);
}