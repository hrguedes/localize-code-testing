using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrguedes.Localize.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                schema: "Operacao",
                table: "Cobrancas",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cobrancas_ClienteId",
                schema: "Operacao",
                table: "Cobrancas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_Clientes_ClienteId",
                schema: "Operacao",
                table: "Cobrancas",
                column: "ClienteId",
                principalSchema: "Operacao",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_Clientes_ClienteId",
                schema: "Operacao",
                table: "Cobrancas");

            migrationBuilder.DropIndex(
                name: "IX_Cobrancas_ClienteId",
                schema: "Operacao",
                table: "Cobrancas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "Operacao",
                table: "Cobrancas");
        }
    }
}
