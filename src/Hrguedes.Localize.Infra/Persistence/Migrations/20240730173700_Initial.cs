using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrguedes.Localize.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Operacao");

            migrationBuilder.EnsureSchema(
                name: "Sistema");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "Operacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(800)", nullable: false),
                    Documento = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Telefone = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Endereco = table.Column<string>(type: "VARCHAR(800)", nullable: false),
                    RegistroAtivo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    RegistroCriado = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RegistroRemovido = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cobrancas",
                schema: "Operacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(1200)", nullable: false),
                    Valor = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Pago = table.Column<bool>(type: "BIT", nullable: false),
                    RegistroAtivo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    RegistroCriado = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RegistroRemovido = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobrancas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Sistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(355)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    RegistroAtivo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    RegistroCriado = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    RegistroRemovido = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "Operacao");

            migrationBuilder.DropTable(
                name: "Cobrancas",
                schema: "Operacao");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Sistema");
        }
    }
}
