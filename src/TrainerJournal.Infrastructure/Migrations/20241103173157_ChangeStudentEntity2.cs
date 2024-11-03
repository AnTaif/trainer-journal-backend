using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStudentEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6bbf1243-ff7c-4a01-931f-ae14fc266f4d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8fbdf7f9-76f2-4166-b2da-3c96733b734d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ef8ed6da-5753-4023-ba63-2d3d1dc33240"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "KyuUpdatedAt",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("17fac322-6e57-4c99-b007-cbd98804aa53"), "a490f0b4-261a-4fb0-9457-6fb6cc94897f", "User", "USER" },
                    { new Guid("70d75240-a1f5-4ac8-8a06-74f5d5a81d72"), "c861a562-033f-4144-baad-4064d4ce56f4", "Admin", "ADMIN" },
                    { new Guid("c40415e7-9afe-42ba-a27f-aaf8a25da23b"), "34bf01f9-5d57-45c5-a235-4719e621132d", "Trainer", "TRAINER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17fac322-6e57-4c99-b007-cbd98804aa53"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70d75240-a1f5-4ac8-8a06-74f5d5a81d72"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c40415e7-9afe-42ba-a27f-aaf8a25da23b"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "KyuUpdatedAt",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6bbf1243-ff7c-4a01-931f-ae14fc266f4d"), "ed730048-6633-46e6-bc1d-aa7ef79d58a0", "Trainer", "TRAINER" },
                    { new Guid("8fbdf7f9-76f2-4166-b2da-3c96733b734d"), "4c17a6ce-a737-425b-b1a2-439320238ca5", "Admin", "ADMIN" },
                    { new Guid("ef8ed6da-5753-4023-ba63-2d3d1dc33240"), "c20be062-22e5-4452-b054-b4daab43b474", "User", "USER" }
                });
        }
    }
}
