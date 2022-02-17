using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Area92.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnded",
                table: "Animes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeasons",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4287be31-a979-4601-9c28-960851bd564b"),
                column: "NumberOfSeasons",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                column: "NumberOfSeasons",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                columns: new[] { "IsEnded", "NumberOfSeasons" },
                values: new object[] { true, 5 });

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                column: "NumberOfSeasons",
                value: 4);

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "Id", "IMDBRating", "IsEnded", "NumberOfSeasons", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"), 8.8000000000000007, false, 2, 2015, "One-Punch Man" },
                    { new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"), 8.3000000000000007, true, 22, 2002, "Naruto" },
                    { new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"), 7.7000000000000002, false, 2, 2021, "The Case Study of Vanitas" },
                    { new Guid("e53409c7-9825-4aaf-a05c-51690d386073"), 8.4000000000000004, false, 1, 2022, "My Dress-Up Darling" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("e53409c7-9825-4aaf-a05c-51690d386073"));

            migrationBuilder.DropColumn(
                name: "IsEnded",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "NumberOfSeasons",
                table: "Animes");
        }
    }
}
