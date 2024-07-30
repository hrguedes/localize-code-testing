using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hrguedes.Localize.Infra.Persistence;

public sealed class Repository<TEntity, TType> : IRepository<TEntity, TType> where TEntity :  BaseEntity<TType>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _table;

    public Repository(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        _context = context;
        _table = _context.Set<TEntity>();
    }
    
    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
        entity.UltimaAtualizacao = DateTime.Now;
        _context.Set<TEntity>().Update(entity);
    }

    public void Create(TEntity entity)
    {
        entity.RegistroCriado = DateTime.Now;
        _table.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        entity.RegistroRemovido = DateTime.Now;
        _table.Remove(entity);
    }

    public async Task<int> CountAsync() => await _table.CountAsync();

    public async Task CreateAsync(TEntity entity)
    {
        entity.RegistroCriado = DateTime.Now;
        await _table.AddAsync(entity);
    }

    public async Task<EntityEntry<TEntity>> CreateAsyncEntry(TEntity entity)
    {
        entity.RegistroCriado = DateTime.Now;
        return await _table.AddAsync(entity);
    }
}