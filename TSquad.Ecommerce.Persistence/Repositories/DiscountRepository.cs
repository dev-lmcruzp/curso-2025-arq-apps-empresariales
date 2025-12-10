using Microsoft.EntityFrameworkCore;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Persistence.Contexts;

namespace TSquad.Ecommerce.Persistence.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _context;
    public DiscountRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Discounts.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Discount?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Discounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    

    public async Task<bool> InsertAsync(Discount entity)
    {
        await _context.AddAsync(entity);
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateAsync(Discount entity)
    {
        var currentEntity = await _context.Discounts.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == entity.Id);
        
        if (currentEntity == null)
            return await Task.FromResult(false);
        
        currentEntity.Name = entity.Name;
        currentEntity.Description = entity.Description;
        currentEntity.Percent = entity.Percent;
        currentEntity.Status = entity.Status;
        _context.Update(currentEntity);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var currentEntity = await _context.Discounts.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == int.Parse(id));
        
        if (currentEntity == null)
            return await Task.FromResult(false);

        _context.Remove(currentEntity);
        return await Task.FromResult(true);
    }
    
    
    public Task<IEnumerable<Discount>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Discount>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Discount?> GetAsync(string id)
    {
        throw new NotImplementedException();
    }
}