using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRdv.Migrations
{
    public partial class migSuppressionColonneIdPraticien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPraticien",
                table: "Rdvs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPraticien",
                table: "Rdvs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
