using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApp.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalEntity",
                columns: table => new
                {
                    IdAnimal = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimalDateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnimalName = table.Column<string>(type: "TEXT", nullable: true),
                    AnimalDescription = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfIndividuals = table.Column<int>(type: "INTEGER", nullable: false),
                    Endangered = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalEntity", x => x.IdAnimal);
                });

            migrationBuilder.CreateTable(
                name: "TreeEntity",
                columns: table => new
                {
                    IdTree = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TreeDateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TreeName = table.Column<string>(type: "TEXT", nullable: true),
                    TreeDescription = table.Column<string>(type: "TEXT", nullable: true),
                    LeafDescription = table.Column<string>(type: "TEXT", nullable: true),
                    MaxHeight = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeEntity", x => x.IdTree);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalEntity");

            migrationBuilder.DropTable(
                name: "TreeEntity");
        }
    }
}
