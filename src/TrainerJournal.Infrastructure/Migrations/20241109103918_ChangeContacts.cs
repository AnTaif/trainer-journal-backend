using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraContacts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("369654e4-3755-4b53-8f72-ddc7d1ef1793"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9bb2fccc-7766-4e06-9857-7f19541fd4d8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bd16460e-dbc5-4a77-9035-2903bed3b515"));

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Relation = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    StudentUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Students_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Students",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3fcc5222-485e-4206-b4ff-8e2317e082e1"), "5791b189-01d1-44ea-bf2f-d32fdfaa6d7d", "Admin", "ADMIN" },
                    { new Guid("79ad5e21-955e-4d99-a5bb-79e954381861"), "0bfb7521-8baa-4c03-82f8-f375d0d41fd7", "User", "USER" },
                    { new Guid("af2979ed-bd7a-4d44-954a-99124d686a90"), "6d79c21c-aef5-49a5-81d4-e27a1e5c468d", "Trainer", "TRAINER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_StudentUserId",
                table: "Contacts",
                column: "StudentUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3fcc5222-485e-4206-b4ff-8e2317e082e1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("79ad5e21-955e-4d99-a5bb-79e954381861"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af2979ed-bd7a-4d44-954a-99124d686a90"));

            migrationBuilder.CreateTable(
                name: "ExtraContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Contact = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StudentUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraContacts_Students_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Students",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("369654e4-3755-4b53-8f72-ddc7d1ef1793"), "2c544c90-a60d-4abd-9dc2-eb932cf00bf9", "User", "USER" },
                    { new Guid("9bb2fccc-7766-4e06-9857-7f19541fd4d8"), "4d3f0b5e-cb71-4b17-9046-f74a2650ada9", "Trainer", "TRAINER" },
                    { new Guid("bd16460e-dbc5-4a77-9035-2903bed3b515"), "8d682b4c-496a-4fb9-af04-6ea4d7841f08", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraContacts_StudentUserId",
                table: "ExtraContacts",
                column: "StudentUserId");
        }
    }
}
