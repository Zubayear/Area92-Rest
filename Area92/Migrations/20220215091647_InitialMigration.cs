using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Area92.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    IMDBRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IMDBRating", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { new Guid("4287be31-a979-4601-9c28-960851bd564b"), 8.6999999999999993, 2020, "Jujutsu Kaisen" },
                    { new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"), 8.6999999999999993, 2019, "Demon Slayer: Kimetsu no Yaiba" },
                    { new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"), 9.0, 2011, "Hunter × Hunter" },
                    { new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"), 9.0, 2013, "Attack on Titan" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
