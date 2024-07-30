using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Hrguedes.Localize.Infra.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    DbSet<Cliente> Clientes { get; set; }
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<Cobranca> Cobrancas { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly);
    }
}