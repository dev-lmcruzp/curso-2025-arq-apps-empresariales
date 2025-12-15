using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Users.Queries;

public sealed record SignInQuery : IRequest<Response<TokenDto>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}