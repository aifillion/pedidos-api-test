using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pedidos.API.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoDelPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoDelPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroPedido = table.Column<int>(type: "int", nullable: true),
                    CicloDelPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoDeContratoInterno = table.Column<long>(type: "bigint", nullable: false),
                    EstadoDelPedidoId = table.Column<int>(type: "int", nullable: false),
                    CuentaCorriente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuando = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_EstadoDelPedido_EstadoDelPedidoId",
                        column: x => x.EstadoDelPedidoId,
                        principalTable: "EstadoDelPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EstadoDelPedido",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "CREADO" },
                    { 2, "ASIGNADO" },
                    { 3, "CERRADO" },
                    { 4, "RECHAZADO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoDelPedidoId",
                table: "Pedidos",
                column: "EstadoDelPedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "EstadoDelPedido");
        }
    }
}
