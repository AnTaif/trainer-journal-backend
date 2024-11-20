using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToAttendanceMark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5f652c24-bb4d-4125-aced-ce3da67392f5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63d88f8c-0d63-4fa4-9b4e-27a7bfe7761e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acd7c499-1827-4ef4-98b9-e1902d720dc6"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AttendanceMarks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3330e792-d8d0-41e0-bbc1-4d9426d0da4a"), "035d0337-0bf8-4dea-886c-b72f3a7219af", "User", "USER" },
                    { new Guid("6a32ce17-505d-431e-bd8b-89f55ed4ed48"), "639a5915-b173-495d-92f8-9a7083dba16e", "Trainer", "TRAINER" },
                    { new Guid("8f9f87ab-fcb2-4dc1-8640-e2ddce737e4f"), "e83f6c41-54be-489c-a91e-f016e884b672", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3330e792-d8d0-41e0-bbc1-4d9426d0da4a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6a32ce17-505d-431e-bd8b-89f55ed4ed48"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8f9f87ab-fcb2-4dc1-8640-e2ddce737e4f"));

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AttendanceMarks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5f652c24-bb4d-4125-aced-ce3da67392f5"), "1ef67393-36f5-4993-a1a1-ec610c9769e4", "User", "USER" },
                    { new Guid("63d88f8c-0d63-4fa4-9b4e-27a7bfe7761e"), "2445418e-34e0-4905-b4f5-566a9476497d", "Admin", "ADMIN" },
                    { new Guid("acd7c499-1827-4ef4-98b9-e1902d720dc6"), "09933f3d-329d-432e-8a3e-d4421832890d", "Trainer", "TRAINER" }
                });
        }
    }
}
