using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace criptoApiProyecto.Migrations
{
    /// <inheritdoc />
    public partial class AddTransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_IdCliente",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Transacciones",
                newName: "CryptoCode");

            migrationBuilder.RenameColumn(
                name: "PrecioUnitario",
                table: "Transacciones",
                newName: "Money");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Transacciones",
                newName: "CryptoAmount");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Transacciones",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Transacciones",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Criptomoneda",
                table: "Transacciones",
                newName: "Action");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_IdCliente",
                table: "Transacciones",
                newName: "IX_Transacciones_ClienteId");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clientes",
                newName: "name");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "Money",
                table: "Transacciones",
                newName: "PrecioUnitario");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transacciones",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "CryptoCode",
                table: "Transacciones",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "CryptoAmount",
                table: "Transacciones",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Transacciones",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "Transacciones",
                newName: "Criptomoneda");

            migrationBuilder.RenameIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                newName: "IX_Transacciones_IdCliente");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Clientes",
                newName: "Nombre");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_IdCliente",
                table: "Transacciones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
