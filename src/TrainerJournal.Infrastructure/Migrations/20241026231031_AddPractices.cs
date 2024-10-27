using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPractices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Session_SessionId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b7fc97e-c04a-4f14-8f5d-2f22d82ef538"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c29ae6a8-9a9a-4511-982e-2c5d3e0ddd4f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e882c4ba-9337-455a-8ca9-2b4799c2c4f6"));

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Visits",
                newName: "PracticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_SessionId",
                table: "Visits",
                newName: "IX_Visits_PracticeId");

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    CancelComment = table.Column<string>(type: "text", nullable: false),
                    PracticeType = table.Column<int>(type: "integer", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    HallId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practices_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Practices_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Practices_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4bffe4c8-0140-4b1b-af78-9c1b1051bf4c"), "5dcb20cb-d72d-493f-870b-cc658aac7a9e", "User", "USER" },
                    { new Guid("5aaf45b3-b866-429e-8346-03330cceaa46"), "fb875391-a8da-4596-9c65-eeea39bd8e93", "Trainer", "TRAINER" },
                    { new Guid("f615cd30-8802-4af9-a867-eff4df9acaed"), "92b109f2-d8e9-4dd2-885b-dd5ff2fe9e16", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Practices_GroupId",
                table: "Practices",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_HallId",
                table: "Practices",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_TrainerId",
                table: "Practices",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Practices_PracticeId",
                table: "Visits",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Practices_PracticeId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Practices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4bffe4c8-0140-4b1b-af78-9c1b1051bf4c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5aaf45b3-b866-429e-8346-03330cceaa46"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f615cd30-8802-4af9-a867-eff4df9acaed"));

            migrationBuilder.RenameColumn(
                name: "PracticeId",
                table: "Visits",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PracticeId",
                table: "Visits",
                newName: "IX_Visits_SessionId");

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    HallId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CancelComment = table.Column<string>(type: "text", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    SessionType = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1b7fc97e-c04a-4f14-8f5d-2f22d82ef538"), "866ccaec-1571-4192-ab3e-ea2ffe42c363", "Trainer", "TRAINER" },
                    { new Guid("c29ae6a8-9a9a-4511-982e-2c5d3e0ddd4f"), "d22f2b12-4e60-4702-a706-6986ae3b0591", "Admin", "ADMIN" },
                    { new Guid("e882c4ba-9337-455a-8ca9-2b4799c2c4f6"), "53b7b3db-3cc6-450f-9222-b1b9fd7d8aeb", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_GroupId",
                table: "Session",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_HallId",
                table: "Session",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_TrainerId",
                table: "Session",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Session_SessionId",
                table: "Visits",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
