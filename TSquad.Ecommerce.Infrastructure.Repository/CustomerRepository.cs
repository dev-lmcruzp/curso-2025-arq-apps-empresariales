using System.Data;
using Dapper;
using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Infrastructure.Data;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<bool> InsertAsync(Customer customer)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersInsert";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);
        
        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersUpdate";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);
        
        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersDelete";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customerId);
        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<int> CountAsync()
    {
        using var connection = _context.CreateConnection();
        const string query = "Select COUNT(*) FROM Customers;";
        var customers = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
        return customers;
    }

    public async Task<Customer?> GetAsync(string customerId)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersGetByID";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customerId);
        var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersList";
        var customers = await connection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);
        return customers;
    }

    public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersListWithPagination";
        var parameters = new DynamicParameters();
        parameters.Add("PageNumber", pageNumber);
        parameters.Add("PageSize", pageSize);
        var customers = await connection.QueryAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
        return customers;
    }
}
