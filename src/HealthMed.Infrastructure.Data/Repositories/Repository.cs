using HealthMed.Domain.Contracts.Repositories;
using HealthMed.Domain.Entities;
using HealthMed.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthMed.Infrastructure.Data.Repositories;

public abstract class Repository<TEntity>(AppDbContext appDbContext) : IRepository<TEntity> where TEntity : EntityBase
{
    protected DbSet<TEntity> DbSet { get; set; } = appDbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => await DbSet.AddAsync(entity, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    => await DbSet.AddRangeAsync(entities, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await DbSet.ToListAsync(cancellationToken);
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await DbSet.FindAsync(id);

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        => await DbSet.AnyAsync(expression, cancellationToken);

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        => await DbSet.FirstOrDefaultAsync(expression, cancellationToken);
}
