using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.CrossCutting.Common;

public interface IJwtService
{
    string GenerateToken(User user);
}