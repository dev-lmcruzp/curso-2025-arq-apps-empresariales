using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Persistence.Contexts;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ICustomerRepository Customers { get; }
    public IUserRepository Users { get; }
    public ICategoryRepository Categories { get; }
    public IDiscountRepository Discounts { get; }

    public UnitOfWork(ApplicationDbContext context, ICustomerRepository customers, 
        IUserRepository users,  ICategoryRepository categories,  IDiscountRepository discounts)
    {
        Customers = customers;
        Users = users;
        Categories = categories;
        Discounts = discounts;
        _context = context;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken); 
    }
}