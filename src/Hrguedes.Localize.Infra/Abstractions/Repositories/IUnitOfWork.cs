using System.Data;
using Hrguedes.Localize.Domain.Entities;

namespace Hrguedes.Localize.Infra.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();


    public IDbConnection Read { get; }


    #region Repositories
    
    public IRepository<Cliente, int> Clientes { get; }
    public IRepository<Cobranca, int> Cobrancas { get; }
    public IRepository<Usuario, Guid> Usuarios { get; }
    
    #endregion
}