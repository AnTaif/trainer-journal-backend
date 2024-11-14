using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultipleStudentGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_GroupId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7bef7225-c89d-4c95-a0c7-f9762e22044e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b1597744-a60c-4ea9-9b97-596d29ba8c60"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f63d4c54-1b7a-4d32-a83d-780c4b7d69f5"));

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "GroupStudent",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudent", x => new { x.GroupsId, x.StudentsUserId });
                    table.ForeignKey(
                        name: "FK_GroupStudent_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudent_Students_StudentsUserId",
                        column: x => x.StudentsUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1c852a1f-6af7-4a15-b8a4-cac611a0de8e"), "b56b03f6-a039-43cb-acc9-b1648a49153d", "User", "USER" },
                    { new Guid("8efd881c-5f7a-4e95-b216-317ca13ef6ce"), "8d071b5e-7f2e-452e-9a7a-6b2a5087641f", "Admin", "ADMIN" },
                    { new Guid("c6d9e75a-5702-4336-a914-53359a2428d2"), "1b43b7d9-509d-4c68-b78c-ddafeb1c0f65", "Trainer", "TRAINER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudent_StudentsUserId",
                table: "GroupStudent",
                column: "StudentsUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupStudent");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1c852a1f-6af7-4a15-b8a4-cac611a0de8e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8efd881c-5f7a-4e95-b216-317ca13ef6ce"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6d9e75a-5702-4336-a914-53359a2428d2"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7bef7225-c89d-4c95-a0c7-f9762e22044e"), "7a74c28d-2194-4c62-b022-76f5709b9609", "Trainer", "TRAINER" },
                    { new Guid("b1597744-a60c-4ea9-9b97-596d29ba8c60"), "99b47ec5-86ca-4732-96bb-10ef2c12abf0", "User", "USER" },
                    { new Guid("f63d4c54-1b7a-4d32-a83d-780c4b7d69f5"), "ffe15b58-af7f-440f-8498-68d948e849bf", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
