using Hrguedes.Localize.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrguedes.Localize.Infra.Persistence.Configuration;

public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .ToTable("Usuarios", "Sistema")
            .HasKey(e => e.Id);
        
        #region Default Properties
        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();
        
        builder.Property(e => e.RegistroAtivo)
            .HasColumnName("RegistroAtivo")
            .HasColumnType("BIT")
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(e => e.RegistroCriado)
            .HasColumnName("RegistroCriado")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(e => e.UltimaAtualizacao)
            .HasColumnName("UltimaAtualizacao")
            .HasColumnType("DATETIME")
            .IsRequired(false);
        
        builder.Property(e => e.RegistroRemovido)
            .HasColumnName("RegistroRemovido")
            .HasColumnType("DATETIME")
            .IsRequired(false);
        #endregion
        
        builder.Property(e => e.Nome)
            .HasColumnName("Nome")
            .HasColumnType("VARCHAR(355)")
            .IsRequired();
        
        builder.Property(e => e.Senha)
            .HasColumnName("Senha")
            .HasColumnType("NVARCHAR(MAX)")
            .IsRequired();
        
        builder.HasMany<Cliente>(e => e.Clientes)
            .WithOne(e => e.Usuario)
            .HasForeignKey(k => k.UsuarioId);
    }
}