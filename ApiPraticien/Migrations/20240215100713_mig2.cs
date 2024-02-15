using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPraticien.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Praticiens");

            migrationBuilder.DropColumn(
                name: "IdPraticien",
                table: "Praticiens");

            migrationBuilder.RenameColumn(
                name: "NomPatient",
                table: "Praticiens",
                newName: "Specialite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialite",
                table: "Praticiens",
                newName: "NomPatient");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Praticiens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdPraticien",
                table: "Praticiens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
