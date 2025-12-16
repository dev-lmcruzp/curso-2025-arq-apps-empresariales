using System.Security.Claims;
using TSquad.Ecommerce.Application.Interface.Presentation;
using TSquad.Ecommerce.Application.UseCases.Commons.Constants;

namespace TSquad.Ecommerce.Services.WebApi.Services;

/// <inheritdoc />
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    /// <inheritdoc />
    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                            GlobalConstant.DefaultUserId;

    /// <inheritdoc />
    public string UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ??
                              GlobalConstant.DefaultUserName;
}