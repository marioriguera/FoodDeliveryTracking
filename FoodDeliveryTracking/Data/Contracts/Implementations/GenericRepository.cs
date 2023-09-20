using Microsoft.EntityFrameworkCore;
using FoodDeliveryTracking.Data.Contracts;
using System.Linq.Expressions;
using FoodDeliveryTracking.Data.Models;
using FoodDeliveryTracking.Data.Context;

/// <summary>
/// Implementation of a generic repository for CRUD operations on entities using Entity Framework.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDC _context;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The DbContext instance used for database operations.</param>
    public GenericRepository(ApplicationDC context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    /// <inheritdoc />
    public IQueryable<TEntity> AsQueryable()
    {
        return (IQueryable<TEntity>)(_dbSet as DbSet<User>).AsQueryable();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await AsQueryable().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await AsQueryable().Where(filter).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await AsQueryable().FirstOrDefaultAsync(predicate);
    }

    /// <inheritdoc />
    public async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
}
