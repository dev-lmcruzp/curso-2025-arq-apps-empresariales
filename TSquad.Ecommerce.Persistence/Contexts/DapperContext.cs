using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TSquad.Ecommerce.Persistence.Contexts;

public class DapperContext
{
    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("NorthwindConnection")
                            ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}