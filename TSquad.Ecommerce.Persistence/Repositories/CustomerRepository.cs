using System.Data;
using Dapper;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Domain;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Persistence.Contexts;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<bool> InsertAsync(Customer entity)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersInsert";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", entity.CustomerId);
        parameters.Add("CompanyName", entity.CompanyName);
        parameters.Add("ContactName", entity.ContactName);
        parameters.Add("ContactTitle", entity.ContactTitle);
        parameters.Add("Address", entity.Address);
        parameters.Add("City", entity.City);
        parameters.Add("Region", entity.Region);
        parameters.Add("PostalCode", entity.PostalCode);
        parameters.Add("Country", entity.Country);
        parameters.Add("Phone", entity.Phone);
        parameters.Add("Fax", entity.Fax);
        
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

    public async Task<bool> DeleteAsync(string id)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersDelete";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", id);
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

    public async Task<Customer?> GetAsync(string id)
    {
        using var connection = _context.CreateConnection();
        const string query = "CustomersGetByID";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", id);
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
