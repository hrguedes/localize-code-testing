using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrguedes.Localize.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relationships_users_to_clients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                schema: "Operacao",
                table: "Clientes",
                type: "UNIQUEIDENTIFIER",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                schema: "Operacao",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioId",
                schema: "Operacao",
                table: "Clientes",
                column: "UsuarioId",
                principalSchema: "Sistema",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioId",
                schema: "Operacao",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioId",
                schema: "Operacao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Operacao",
                table: "Clientes");
        }
    }
}
