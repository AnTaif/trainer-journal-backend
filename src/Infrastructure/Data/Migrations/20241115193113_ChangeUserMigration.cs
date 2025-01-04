using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c2c6c03-48fb-4ad2-aae0-b2f009ecc37d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a95549fd-fdf6-498a-8390-2779f7b1d534"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d8ca5f69-cdf7-43ca-a051-ae3184295815"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Trainers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Trainers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("07067430-3e18-4cce-a632-2cc2fad11fd8"), "0ac9cb98-534c-4999-b21d-c8b526725650", "Trainer", "TRAINER" },
                    { new Guid("4156af5b-4d50-4463-a80d-b85eb3817e4c"), "e29e75ff-a7ab-4d9b-9e8e-6789e96ee1c5", "User", "USER" },
                    { new Guid("86959f5b-79e1-402b-803c-1865a51addf3"), "7c0cb4f7-a555-4bda-8317-5c7355ab1af7", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("07067430-3e18-4cce-a632-2cc2fad11fd8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4156af5b-4d50-4463-a80d-b85eb3817e4c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("86959f5b-79e1-402b-803c-1865a51addf3"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6c2c6c03-48fb-4ad2-aae0-b2f009ecc37d"), "07d84805-5977-4b57-9eaf-e0230c4f3fc4", "User", "USER" },
                    { new Guid("a95549fd-fdf6-498a-8390-2779f7b1d534"), "6cc26b7c-0379-470e-a7bf-3b97139ca86f", "Admin", "ADMIN" },
                    { new Guid("d8ca5f69-cdf7-43ca-a051-ae3184295815"), "7c07888a-de42-474f-afa9-84036887e733", "Trainer", "TRAINER" }
                });
        }
    }
}
