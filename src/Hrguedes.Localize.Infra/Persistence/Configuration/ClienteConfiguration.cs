using Hrguedes.Localize.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrguedes.Localize.Infra.Persistence.Configuration;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder
            .ToTable("Clientes", "Operacao")
            .HasKey(e => e.Id);

        #region Default Properties
        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .HasColumnType("INT")
            .IsRequired()
            .UseIdentityColumn();
        
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
            .HasColumnType("VARCHAR(800)")
            .IsRequired();
        
        builder.Property(e => e.Documento)
            .HasColumnName("Documento")
            .HasColumnType("VARCHAR(14)")
            .IsRequired();
        
        builder.Property(e => e.Telefone)
            .HasColumnName("Telefone")
            .HasColumnType("VARCHAR(14)")
            .IsRequired();
        
        builder.Property(e => e.Endereco)
            .HasColumnName("Endereco")
            .HasColumnType("VARCHAR(800)")
            .IsRequired();

        builder.HasMany<Cobranca>(e => e.Cobrancas)
            .WithOne(e => e.Cliente)
            .HasForeignKey(e => e.ClienteId);
        
        builder.HasOne<Usuario>(e => e.Usuario)
            .WithMany(e => e.Clientes)
            .HasForeignKey(k => k.UsuarioId);
    }
}