using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceMark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

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
                name: "AttendanceMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AttendanceMarks_PracticeId",
                table: "AttendanceMarks",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_StudentId",
                table: "AttendanceMarks",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceMarks");

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

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Visits_PracticeId",
                table: "Visits",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_StudentId",
                table: "Visits",
                column: "StudentId");
        }
    }
}
