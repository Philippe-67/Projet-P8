using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRdv.Migrations
{
    public partial class mignew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rdvs_Calendriers_CalendrierId",
                table: "Rdvs");

            migrationBuilder.DropTable(
                name: "Calendriers");

            migrationBuilder.DropIndex(
                name: "IX_Rdvs_CalendrierId",
                table: "Rdvs");

            migrationBuilder.DropColumn(
                name: "CalendrierId",
                table: "Rdvs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalendrierId",
                table: "Rdvs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Calendriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPraticien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendriers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_CalendrierId",
                table: "Rdvs",
                column: "CalendrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rdvs_Calendriers_CalendrierId",
                table: "Rdvs",
                column: "CalendrierId",
                principalTable: "Calendriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
