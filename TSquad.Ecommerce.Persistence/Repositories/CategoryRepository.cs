using System.Data;
using Dapper;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Domain;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Persistence.Contexts;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;

    public CategoryRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        const string query = "SELECT * FROM Categories";
        var categories = await connection.QueryAsync<Category>(query, commandType: CommandType.Text);
        return categories;
    }
}