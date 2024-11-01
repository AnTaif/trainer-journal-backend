using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Halls_HallId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Practices_PracticeId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practices",
                table: "Practices");

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

            migrationBuilder.RenameTable(
                name: "Practices",
                newName: "Practice");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Practice",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Practice",
                newName: "End");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_TrainerId",
                table: "Practice",
                newName: "IX_Practice_TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_HallId",
                table: "Practice",
                newName: "IX_Practice_HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Practices_GroupId",
                table: "Practice",
                newName: "IX_Practice_GroupId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCanceled",
                table: "Practice",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "CancelComment",
                table: "Practice",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Practice",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OverridenPracticeId",
                table: "Practice",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Until",
                table: "Practice",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practice",
                table: "Practice",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1ac5a3e5-f16d-462d-802a-683c7cb01ac8"), "ed644f8b-a46e-4b82-98d7-eda39fb5b596", "Admin", "ADMIN" },
                    { new Guid("38ac2e32-4799-4a77-9631-d0f9867c1f02"), "45fff879-8f19-4b2f-bfda-d449392dc58b", "Trainer", "TRAINER" },
                    { new Guid("7af7aa64-b489-43de-a655-9050e8b099dc"), "46f31bce-be12-4ce0-af5e-8347cfbf6c61", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Practice_OverridenPracticeId",
                table: "Practice",
                column: "OverridenPracticeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Practice_OverridenPracticeId",
                table: "Practice");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ac5a3e5-f16d-462d-802a-683c7cb01ac8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("38ac2e32-4799-4a77-9631-d0f9867c1f02"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7af7aa64-b489-43de-a655-9050e8b099dc"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Practice");

            migrationBuilder.DropColumn(
                name: "OverridenPracticeId",
                table: "Practice");

            migrationBuilder.DropColumn(
                name: "Until",
                table: "Practice");

            migrationBuilder.RenameTable(
                name: "Practice",
                newName: "Practices");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Practices",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Practices",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_TrainerId",
                table: "Practices",
                newName: "IX_Practices_TrainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_HallId",
                table: "Practices",
                newName: "IX_Practices_HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Practice_GroupId",
                table: "Practices",
                newName: "IX_Practices_GroupId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCanceled",
                table: "Practices",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CancelComment",
                table: "Practices",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practices",
                table: "Practices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("623a7c20-bafa-4855-b2a4-01541e4432f5"), "06db41b8-9ab6-4354-a158-1f35fa5d411d", "Trainer", "TRAINER" },
                    { new Guid("838b3b43-9cc5-40e1-8202-71371250eff5"), "a66ece2f-c3e8-4862-8684-da7b5b30b378", "User", "USER" },
                    { new Guid("bd9da78d-3349-4e39-9af0-4138868fdbe4"), "ded9dbfb-9a15-4014-822b-6e588d305c15", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Halls_HallId",
                table: "Practices",
                column: "HallId",
                principalTable: "Halls",
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
    }
}
