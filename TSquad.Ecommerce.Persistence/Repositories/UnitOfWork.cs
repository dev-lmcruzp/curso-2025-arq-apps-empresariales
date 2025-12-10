using TSquad.Ecommerce.Application.Interface.Persistence;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICustomerRepository Customers { get; }
    public IUserRepository Users { get; }
    public ICategoryRepository Categories { get; }
    
    public UnitOfWork(ICustomerRepository customers, IUserRepository users,  ICategoryRepository categories)
    {
        Customers = customers;
        Users = users;
        Categories = categories;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}