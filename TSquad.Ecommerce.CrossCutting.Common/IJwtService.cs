using TSquad.Ecommerce.Domain;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.CrossCutting.Common;

public interface IJwtService
{
    string GenerateToken(User user);
}