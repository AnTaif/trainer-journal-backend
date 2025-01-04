using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameStudentUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Students_StudentUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_Students_StudentsUserId",
                table: "GroupStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_UserId",
                table: "Trainers");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Trainers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StudentsUserId",
                table: "GroupStudent",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupStudent_StudentsUserId",
                table: "GroupStudent",
                newName: "IX_GroupStudent_StudentsId");

            migrationBuilder.RenameColumn(
                name: "StudentUserId",
                table: "Contacts",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_StudentUserId",
                table: "Contacts",
                newName: "IX_Contacts_StudentId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6afef4ca-abee-4841-ae6c-0cecf638468f"), "90bd2d45-06bc-447b-a961-944a381e1729", "User", "USER" },
                    { new Guid("7350aec2-685e-480c-b23b-e23c8767291c"), "10c115b2-1846-4826-8e81-c0bb8af3f7fd", "Admin", "ADMIN" },
                    { new Guid("af047d79-eff5-44b9-b09a-a938d8072e2f"), "432e0f98-ea90-424c-9d38-e21351d7a6ef", "Trainer", "TRAINER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Students_StudentId",
                table: "Contacts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_Students_StudentsId",
                table: "GroupStudent",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_Id",
                table: "Students",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_Id",
                table: "Trainers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Students_StudentId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_Students_StudentsId",
                table: "GroupStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_Id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_Id",
                table: "Trainers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6afef4ca-abee-4841-ae6c-0cecf638468f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7350aec2-685e-480c-b23b-e23c8767291c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af047d79-eff5-44b9-b09a-a938d8072e2f"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trainers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "GroupStudent",
                newName: "StudentsUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupStudent_StudentsId",
                table: "GroupStudent",
                newName: "IX_GroupStudent_StudentsUserId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Contacts",
                newName: "StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_StudentId",
                table: "Contacts",
                newName: "IX_Contacts_StudentUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3330e792-d8d0-41e0-bbc1-4d9426d0da4a"), "035d0337-0bf8-4dea-886c-b72f3a7219af", "User", "USER" },
                    { new Guid("6a32ce17-505d-431e-bd8b-89f55ed4ed48"), "639a5915-b173-495d-92f8-9a7083dba16e", "Trainer", "TRAINER" },
                    { new Guid("8f9f87ab-fcb2-4dc1-8640-e2ddce737e4f"), "e83f6c41-54be-489c-a91e-f016e884b672", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Students_StudentUserId",
                table: "Contacts",
                column: "StudentUserId",
                principalTable: "Students",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_Students_StudentsUserId",
                table: "GroupStudent",
                column: "StudentsUserId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_UserId",
                table: "Trainers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
