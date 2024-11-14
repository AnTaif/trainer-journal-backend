using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHallAddressToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "HallAddress",
                table: "Practices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HallAddress",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30d2bf57-a067-4253-847e-7647872bd80f"), "935f6790-c789-4604-974e-10ca1f756fdc", "Trainer", "TRAINER" },
                    { new Guid("5c31c0df-790f-41bc-94ad-6aacd3ebb55d"), "908087f7-910b-4769-92ff-8390d540c96f", "User", "USER" },
                    { new Guid("aa8a1756-e8f9-4541-ad3e-930e33bb1228"), "9b28b97d-49bc-4589-993e-02612fec16b5", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30d2bf57-a067-4253-847e-7647872bd80f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5c31c0df-790f-41bc-94ad-6aacd3ebb55d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aa8a1756-e8f9-4541-ad3e-930e33bb1228"));

            migrationBuilder.DropColumn(
                name: "HallAddress",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "HallAddress",
                table: "Groups");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1c852a1f-6af7-4a15-b8a4-cac611a0de8e"), "b56b03f6-a039-43cb-acc9-b1648a49153d", "User", "USER" },
                    { new Guid("8efd881c-5f7a-4e95-b216-317ca13ef6ce"), "8d071b5e-7f2e-452e-9a7a-6b2a5087641f", "Admin", "ADMIN" },
                    { new Guid("c6d9e75a-5702-4336-a914-53359a2428d2"), "1b43b7d9-509d-4c68-b78c-ddafeb1c0f65", "Trainer", "TRAINER" }
                });
        }
    }
}
