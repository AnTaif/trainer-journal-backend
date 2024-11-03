using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("609db2d4-04f2-464d-b90d-6744b721ce2d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1d9c636-e746-4ea6-bcff-76eaec4790f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cd812b3b-4e7f-4fd8-8c05-b4ce5cd5e467"));

            migrationBuilder.AlterColumn<int>(
                name: "SchoolGrade",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Kyu",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "SchoolGrade",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Kyu",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("609db2d4-04f2-464d-b90d-6744b721ce2d"), "460a20f2-45d9-44ef-a61a-4803ba298881", "Admin", "ADMIN" },
                    { new Guid("a1d9c636-e746-4ea6-bcff-76eaec4790f2"), "89160391-fca7-4ec8-9bf7-7c9dd2b619f6", "User", "USER" },
                    { new Guid("cd812b3b-4e7f-4fd8-8c05-b4ce5cd5e467"), "c2dbebfc-1667-4ec8-b273-f6299b00d4cf", "Trainer", "TRAINER" }
                });
        }
    }
}
