namespace TSquad.Ecommerce.Application.Interface.Persistence;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IUserRepository Users { get; }
    ICategoryRepository Categories { get; }
}