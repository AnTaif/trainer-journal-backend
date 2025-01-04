using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePaymentReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2b0b61d9-2855-4d5d-a8d8-3a38fba2500c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("92e49a67-c0d5-4362-819e-2609845d312e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bdd75d68-b171-425c-b81f-29518cb91680"));

            migrationBuilder.RenameColumn(
                name: "IsChecked",
                table: "PaymentReceipts",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "CheckDate",
                table: "PaymentReceipts",
                newName: "VerificationDate");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "VerificationDate",
                table: "PaymentReceipts",
                newName: "CheckDate");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "PaymentReceipts",
                newName: "IsChecked");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2b0b61d9-2855-4d5d-a8d8-3a38fba2500c"), "ee294738-8097-44ec-9625-e5c02ec7990f", "User", "USER" },
                    { new Guid("92e49a67-c0d5-4362-819e-2609845d312e"), "a93a349b-6932-4831-9505-b49b95c4c507", "Trainer", "TRAINER" },
                    { new Guid("bdd75d68-b171-425c-b81f-29518cb91680"), "df803940-4637-4ee0-886c-b70223509344", "Admin", "ADMIN" }
                });
        }
    }
}
