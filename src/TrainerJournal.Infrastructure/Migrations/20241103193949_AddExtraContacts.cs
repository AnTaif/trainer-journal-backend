using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FirstParent_Contact",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstParent_Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondParent_Contact",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondParent_Name",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolGrade",
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

            migrationBuilder.CreateTable(
                name: "ExtraContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraContacts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("37327015-9230-4e23-b41f-d4b3769c654a"), "4d625191-3656-4b55-8d0b-ae9b803240b3", "User", "USER" },
                    { new Guid("a3b2a3b2-e33a-4f0c-b958-048110f6c8c2"), "415f7f2c-8058-4ec3-ab2e-92fc7cfc74bb", "Admin", "ADMIN" },
                    { new Guid("b2a9e829-5d33-4740-ae68-28a0088859d0"), "c608ed7b-a223-4017-975e-b0f6c20e8a20", "Trainer", "TRAINER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraContacts_StudentId",
                table: "ExtraContacts",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraContacts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("37327015-9230-4e23-b41f-d4b3769c654a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a3b2a3b2-e33a-4f0c-b958-048110f6c8c2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2a9e829-5d33-4740-ae68-28a0088859d0"));

            migrationBuilder.AlterColumn<int>(
                name: "SchoolGrade",
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

            migrationBuilder.AddColumn<string>(
                name: "FirstParent_Contact",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstParent_Name",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondParent_Contact",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondParent_Name",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

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
    }
}
