using HealthMed.AgendamentoService.Application.Contracts.Persistence;
using HealthMed.AgendamentoService.Infrastructure.Persistence;
using HealthMed.BuildingBlocks.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthMed.AgendamentoService.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : EntityBase
{
    protected DbSet<T> DbSet { get; set; }
    protected readonly AgendamentoContext AgendamentoContext;

    public Repository(AgendamentoContext agendamentoContext)
    {
        DbSet = agendamentoContext.Set<T>();
        AgendamentoContext = agendamentoContext;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = DbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = DbSet;
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
        DbSet.Add(entity);
        await AgendamentoContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        AgendamentoContext.Entry(entity).State = EntityState.Modified;
        await AgendamentoContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await AgendamentoContext.SaveChangesAsync();
    }
}
