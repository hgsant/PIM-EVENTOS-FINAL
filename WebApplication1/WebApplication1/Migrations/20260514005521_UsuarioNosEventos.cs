using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventosPRO.Web.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioNosEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "eventos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "eventos");
        }
    }
}
