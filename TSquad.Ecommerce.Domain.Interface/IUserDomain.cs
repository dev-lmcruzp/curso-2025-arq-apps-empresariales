using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.Domain.Interface;

public interface IUserDomain
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> InsertAsync(User user, string  password);
    Task<bool> CheckPassword(User user, string  passwordHash);
}
