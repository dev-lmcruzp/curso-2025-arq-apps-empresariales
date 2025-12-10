using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.Interface.Persistence;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> InsertAsync(User user, string  password);
    Task<bool> CheckPassword(User user, string  passwordHash);
}