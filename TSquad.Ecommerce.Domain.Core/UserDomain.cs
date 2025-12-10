using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Domain.Interface;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Domain.Core;

public class UserDomain : IUserDomain
{
    private readonly IUnitOfWork _unitOfWork;

    public UserDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _unitOfWork.Users.GetByEmailAsync(email);
    }

    public async Task<bool> InsertAsync(User user, string password)
    {
        return await _unitOfWork.Users.InsertAsync(user, password);
    }

    public async Task<bool> CheckPassword(User user, string passwordHash)
    {
        return await _unitOfWork.Users.CheckPassword(user, passwordHash);
    }
}