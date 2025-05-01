using HealthMed.BuildingBlocks.Common;
using System.Linq.Expressions;

namespace HealthMed.PacienteService.Application.Contracts.Persistence;

public interface IRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    string? includeString = null,
                                    bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                   List<Expression<Func<T, object>>>? includes = null,
                                   bool disableTracking = true);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
