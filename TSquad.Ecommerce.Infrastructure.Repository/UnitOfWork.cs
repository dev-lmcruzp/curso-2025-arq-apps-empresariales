using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Infrastructure.Repository;

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