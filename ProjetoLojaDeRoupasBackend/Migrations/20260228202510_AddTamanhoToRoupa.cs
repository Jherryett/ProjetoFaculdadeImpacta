using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLojaDeRoupas.Migrations
{
    /// <inheritdoc />
    public partial class AddTamanhoToRoupa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tamanho",
                table: "Roupas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Roupas");
        }
    }
}
