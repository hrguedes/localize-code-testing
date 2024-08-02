using System.Data;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;

namespace Hrguedes.Localize.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly DbSession _session;
    
    public UnitOfWork(ApplicationDbContext context, DbSession session)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(context));
        ArgumentException.ThrowIfNullOrEmpty(nameof(session));

        _context = context;
        _session = session;
    }

    #region Repos

    public IRepository<Cliente, int> Clientes => new Repository<Cliente, int>(_context);
    public IRepository<Cobranca, int> Cobrancas => new Repository<Cobranca, int>(_context);
    public IRepository<Usuario, Guid> Usuarios => new Repository<Usuario, Guid>(_context);
    
    #endregion

    
    
    public void Commit()
    {
        _session.Transaction.Commit();
        Dispose();
    }
    public void Rollback()
    {
        _session.Transaction.Rollback();
        Dispose();
    }
    
    public IDbConnection Read => _session.Connection;
    public void SaveChanges() => _context.SaveChanges();
    public void Dispose() => _session.Transaction?.Dispose();
    public void BeginTransaction() => _session.Transaction = _session.Connection.BeginTransaction();
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
}