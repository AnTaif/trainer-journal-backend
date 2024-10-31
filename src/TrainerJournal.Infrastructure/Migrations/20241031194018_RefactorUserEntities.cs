using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUserEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04237cfc-e5f5-4e25-b0f6-f3785ffc7e08"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a3e9d0a-a92d-402c-b2e5-a89d57c17df3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4844ef0-032f-4997-ad91-280f555cb60e"));

            migrationBuilder.DropColumn(
                name: "firstParentContact",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "firstParentName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "secondParentContact",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "secondParentName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "AspNetUsers",
                newName: "FullName_MiddleName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FullName_FirstName");

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
                    { new Guid("623a7c20-bafa-4855-b2a4-01541e4432f5"), "06db41b8-9ab6-4354-a158-1f35fa5d411d", "Trainer", "TRAINER" },
                    { new Guid("838b3b43-9cc5-40e1-8202-71371250eff5"), "a66ece2f-c3e8-4862-8684-da7b5b30b378", "User", "USER" },
                    { new Guid("bd9da78d-3349-4e39-9af0-4138868fdbe4"), "ded9dbfb-9a15-4014-822b-6e588d305c15", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("623a7c20-bafa-4855-b2a4-01541e4432f5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("838b3b43-9cc5-40e1-8202-71371250eff5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bd9da78d-3349-4e39-9af0-4138868fdbe4"));

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

            migrationBuilder.RenameColumn(
                name: "FullName_MiddleName",
                table: "AspNetUsers",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "FullName_LastName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName_FirstName",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "firstParentContact",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstParentName",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "secondParentContact",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "secondParentName",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("04237cfc-e5f5-4e25-b0f6-f3785ffc7e08"), "7860ffd9-536b-41c4-84cf-80cf480ba521", "Trainer", "TRAINER" },
                    { new Guid("2a3e9d0a-a92d-402c-b2e5-a89d57c17df3"), "66c829c3-d66b-4306-beb8-851fcf299af5", "Admin", "ADMIN" },
                    { new Guid("c4844ef0-032f-4997-ad91-280f555cb60e"), "37c37686-d66a-4ed3-a866-6ff43c456e52", "User", "USER" }
                });
        }
    }
}
