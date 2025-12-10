namespace TSquad.Ecommerce.Infrastructure.Interface;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
}