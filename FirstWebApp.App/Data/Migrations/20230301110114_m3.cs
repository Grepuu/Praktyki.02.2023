using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebApp.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionEntity",
                columns: table => new
                {
                    IdPermission = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PermissionDateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PermissionName = table.Column<string>(type: "TEXT", nullable: true),
                    PermissionDescription = table.Column<string>(type: "TEXT", nullable: true),
                    SinceWhen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UntilWhen = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntity", x => x.IdPermission);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionEntity");
        }
    }
}
