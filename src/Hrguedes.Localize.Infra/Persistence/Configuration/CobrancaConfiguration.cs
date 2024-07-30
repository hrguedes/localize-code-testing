using Hrguedes.Localize.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrguedes.Localize.Infra.Persistence.Configuration;

public sealed class CobrancaConfiguration : IEntityTypeConfiguration<Cobranca>
{
    public void Configure(EntityTypeBuilder<Cobranca> builder)
    {
        builder
            .ToTable("Cobrancas", "Operacao")
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
        
        builder.Property(e => e.Descricao)
            .HasColumnName("Descricao")
            .HasColumnType("VARCHAR(1200)")
            .IsRequired();
        
        builder.Property(e => e.Valor)
            .HasColumnName("Valor")
            .HasColumnType("DECIMAL(12,2)")
            .IsRequired();
        
        builder.Property(e => e.DataVencimento)
            .HasColumnName("DataVencimento")
            .HasColumnType("DATETIME")
            .IsRequired();
        
        builder.Property(e => e.Pago)
            .HasColumnName("Pago")
            .HasColumnType("BIT")
            .IsRequired();

        builder.HasOne<Cliente>(e => e.Cliente)
            .WithMany(e => e.Cobrancas)
            .HasForeignKey(k => k.ClienteId);
    }
}