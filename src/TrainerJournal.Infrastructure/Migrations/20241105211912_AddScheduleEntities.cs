using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practice_Groups_GroupId",
                table: "Practice");

            migrationBuilder.DropForeignKey(
                name: "FK_Practice_Halls_HallId",
                table: "Practice");

            migrationBuilder.DropForeignKey(
                name: "FK_Practice_Practice_OverridenPracticeId",
                table: "Practice");

            migrationBuilder.DropForeignKey(
                name: "FK_Practice_Trainers_TrainerId",
                table: "Practice");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Practice_PracticeId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practice",
                table: "Practice");

            migrationBuilder.DropIndex(
                name: "IX_Practice_HallId",
                table: "Practice");

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

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Practice");

            migrationBuilder.RenameTable(
                name: "Practice",
                newName: "Practices");

            migrationBuilder.RenameColumn(
                name: "Until",
                table: "Practices",
                newName: "OriginalStart");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_TrainerId",
                table: "Practices",
                newName: "IX_Practices_TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_OverridenPracticeId",
                table: "Practices",
                newName: "IX_Practices_OverridenPracticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_GroupId",
                table: "Practices",
                newName: "IX_Practices_GroupId");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Groups",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Practices",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practices",
                table: "Practices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Until = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8dace860-2f0f-4e15-af59-316d107183aa"), "0d0db1a3-8ed6-4b2e-9702-c00a9ba725ac", "Admin", "ADMIN" },
                    { new Guid("cdb68881-e124-4dfe-a3ec-e205e5f7732e"), "221669da-66f0-46de-a351-f9f503dab3e6", "User", "USER" },
                    { new Guid("dcde5648-f49b-4c44-afef-b9cf2d557a9d"), "b40483e7-2ea3-448d-860d-7c13d8dee0f2", "Trainer", "TRAINER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Practices_ScheduleId",
                table: "Practices",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Practices_OverridenPracticeId",
                table: "Practices",
                column: "OverridenPracticeId",
                principalTable: "Practices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Schedules_ScheduleId",
                table: "Practices",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Practices_Groups_GroupId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Practices_OverridenPracticeId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Schedules_ScheduleId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Practices_PracticeId",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practices",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_ScheduleId",
                table: "Practices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8dace860-2f0f-4e15-af59-316d107183aa"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cdb68881-e124-4dfe-a3ec-e205e5f7732e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcde5648-f49b-4c44-afef-b9cf2d557a9d"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Practices");

            migrationBuilder.RenameTable(
                name: "Practices",
                newName: "Practice");

            migrationBuilder.RenameColumn(
                name: "OriginalStart",
                table: "Practice",
                newName: "Until");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_TrainerId",
                table: "Practice",
                newName: "IX_Practice_TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_OverridenPracticeId",
                table: "Practice",
                newName: "IX_Practice_OverridenPracticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_GroupId",
                table: "Practice",
                newName: "IX_Practice_GroupId");

            migrationBuilder.AddColumn<Guid>(
                name: "HallId",
                table: "Practice",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practice",
                table: "Practice",
                column: "Id");

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
                name: "IX_Practice_HallId",
                table: "Practice",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practice_Groups_GroupId",
                table: "Practice",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practice_Halls_HallId",
                table: "Practice",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practice_Practice_OverridenPracticeId",
                table: "Practice",
                column: "OverridenPracticeId",
                principalTable: "Practice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Practice_Trainers_TrainerId",
                table: "Practice",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Practice_PracticeId",
                table: "Visits",
                column: "PracticeId",
                principalTable: "Practice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
