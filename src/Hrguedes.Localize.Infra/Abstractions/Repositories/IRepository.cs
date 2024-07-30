using Hrguedes.Localize.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hrguedes.Localize.Infra.Abstractions.Repositories;

public interface IRepository<TEntity, TType> where TEntity : class, IBaseEntity<TType>
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<int> CountAsync();
    Task CreateAsync(TEntity entity);
    Task<EntityEntry<TEntity>> CreateAsyncEntry(TEntity entity);
}