using System.Data;
using Dapper;
using Microsoft.AspNetCore.Identity;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Persistence.Contexts;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRepository(DapperContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _context.CreateConnection();
        const string query = "UsersGetByEmail";
        var parameters = new DynamicParameters();
        parameters.Add("Email", email);
        var user = await connection.QuerySingleOrDefaultAsync<User>(query, parameters, commandType: CommandType.StoredProcedure);
        return user;
    }

    public async Task<bool> InsertAsync(User user, string password)
    {
        using var connection = _context.CreateConnection();
        const string query = "UsersInsert";
        var parameters = new DynamicParameters();
        parameters.Add("Id", Guid.NewGuid().ToString());
        parameters.Add("FirstName", user.FirstName);
        parameters.Add("LastName", user.LastName);
        parameters.Add("Email", user.Email);
        parameters.Add("UserName", user.UserName);
        parameters.Add("PasswordHash", _passwordHasher.HashPassword(user, password));
        
        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<bool> CheckPassword(User user, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, passwordHash);
        return await Task.FromResult(result == PasswordVerificationResult.Success);
    }
}