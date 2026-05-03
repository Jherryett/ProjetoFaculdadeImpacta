using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLojaDeRoupas.Migrations
{
    public partial class AjustesCarrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_ClienteId_RoupaId",
                table: "Carrinhos",
                columns: new[] { "ClienteId", "RoupaId" },
                unique: true);

            
            migrationBuilder.AddCheckConstraint(
                name: "CK_Carrinhos_Quantidade",
                table: "Carrinhos",
                sql: "[Quantidade] > 0");

            
            migrationBuilder.AddCheckConstraint(
                name: "CK_Carrinhos_ValorUnitario",
                table: "Carrinhos",
                sql: "[ValorUnitario] >= 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carrinhos_ClienteId_RoupaId",
                table: "Carrinhos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Carrinhos_Quantidade",
                table: "Carrinhos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Carrinhos_ValorUnitario",
                table: "Carrinhos");
        }
    }
}
