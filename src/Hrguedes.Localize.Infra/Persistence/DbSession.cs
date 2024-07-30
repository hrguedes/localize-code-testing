using System.Data;
using Microsoft.Data.SqlClient;

namespace Hrguedes.Localize.Infra.Persistence;

public sealed class DbSession : IDisposable
{
    private Guid _id;
    public IDbConnection Connection { get; }
    
    public IDbTransaction Transaction { get; set; }

    public DbSession(string connectionString)
    {
        _id = Guid.NewGuid();
        Connection = new SqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();
}