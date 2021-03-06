using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Area92.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenresString",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"),
                column: "GenresString",
                value: "Animation,Action,Comedy,Fantasy,Sci-Fi");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4287be31-a979-4601-9c28-960851bd564b"),
                column: "GenresString",
                value: "Animation,Action,Adventure,Fantasy,Thriller");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                column: "GenresString",
                value: "Animation,Action,Adventure,Fantasy,Thriller");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"),
                column: "GenresString",
                value: "Animation,Action,Adventure,Fantasy,Comedy");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                column: "GenresString",
                value: "Animation,Action,Adventure,Comedy,Fantasy");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"),
                column: "GenresString",
                value: "Animation,Sci-Fi,Mystery,Fantasy,Horror");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                column: "GenresString",
                value: "Animation,Action,Adventure,Drama,Fantasy,Horror");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("e53409c7-9825-4aaf-a05c-51690d386073"),
                column: "GenresString",
                value: "Animation,Comedy,Romance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenresString",
                table: "Animes");
        }
    }
}
