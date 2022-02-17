using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Area92.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IMDBRating",
                table: "Animes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"),
                column: "IMDBRating",
                value: 8.8m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4287be31-a979-4601-9c28-960851bd564b"),
                column: "IMDBRating",
                value: 8.7m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                column: "IMDBRating",
                value: 8.7m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"),
                column: "IMDBRating",
                value: 8.3m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                column: "IMDBRating",
                value: 9.0m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"),
                column: "IMDBRating",
                value: 7.7m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                column: "IMDBRating",
                value: 9.0m);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("e53409c7-9825-4aaf-a05c-51690d386073"),
                column: "IMDBRating",
                value: 8.4m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "IMDBRating",
                table: "Animes",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"),
                column: "IMDBRating",
                value: 8.8000000000000007);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4287be31-a979-4601-9c28-960851bd564b"),
                column: "IMDBRating",
                value: 8.6999999999999993);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                column: "IMDBRating",
                value: 8.6999999999999993);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"),
                column: "IMDBRating",
                value: 8.3000000000000007);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                column: "IMDBRating",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"),
                column: "IMDBRating",
                value: 7.7000000000000002);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                column: "IMDBRating",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Animes",
                keyColumn: "Id",
                keyValue: new Guid("e53409c7-9825-4aaf-a05c-51690d386073"),
                column: "IMDBRating",
                value: 8.4000000000000004);
        }
    }
}
