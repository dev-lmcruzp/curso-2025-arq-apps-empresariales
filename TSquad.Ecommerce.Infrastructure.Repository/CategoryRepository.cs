using System.Data;
using Dapper;
using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Infrastructure.Data;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Infrastructure.Repository;

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