using Hrguedes.Localize.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Hrguedes.Localize.Infra.Persistence;

public class DatabaseInitializerSeed
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Clientes.Any())
        {
            var usuario = new Usuario
            {
                Nome = "Admin",
                Email = "admin@localize.com",
                Senha = "password123",
                RegistroAtivo = true,
                RegistroCriado = DateTime.UtcNow
            };

            context.Usuarios.Add(usuario);

            var cliente = new Cliente
            {
                Nome = "Cliente Teste",
                Documento = "123456789",
                Telefone = "123456789",
                Endereco = "Rua Exemplo, 123",
                UsuarioId = usuario.Id,
                RegistroAtivo = true,
                RegistroCriado = DateTime.UtcNow,
                Usuario = usuario
            };

            context.Clientes.Add(cliente);

            var cobranca = new Cobranca
            {
                Pago = false,
                Valor = 100m,
                Descricao = "Cobrança Teste",
                DataVencimento = DateTime.UtcNow.AddDays(30),
                ClienteId = cliente.Id,
                RegistroAtivo = true,
                RegistroCriado = DateTime.UtcNow,
                Cliente = cliente
            };

            context.Cobrancas.Add(cobranca);

            context.SaveChanges();
        }
    }
}