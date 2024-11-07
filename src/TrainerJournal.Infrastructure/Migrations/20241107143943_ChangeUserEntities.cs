using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraContacts_Students_StudentId",
                table: "ExtraContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Trainers_TrainerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

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
                name: "Id",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ExtraContacts",
                newName: "StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraContacts_StudentId",
                table: "ExtraContacts",
                newName: "IX_ExtraContacts_StudentUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("369654e4-3755-4b53-8f72-ddc7d1ef1793"), "2c544c90-a60d-4abd-9dc2-eb932cf00bf9", "User", "USER" },
                    { new Guid("9bb2fccc-7766-4e06-9857-7f19541fd4d8"), "4d3f0b5e-cb71-4b17-9046-f74a2650ada9", "Trainer", "TRAINER" },
                    { new Guid("bd16460e-dbc5-4a77-9035-2903bed3b515"), "8d682b4c-496a-4fb9-af04-6ea4d7841f08", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraContacts_Students_StudentUserId",
                table: "ExtraContacts",
                column: "StudentUserId",
                principalTable: "Students",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Trainers_TrainerId",
                table: "Groups",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraContacts_Students_StudentUserId",
                table: "ExtraContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Trainers_TrainerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Trainers_TrainerId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Students_StudentId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

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

            migrationBuilder.RenameColumn(
                name: "StudentUserId",
                table: "ExtraContacts",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraContacts_StudentUserId",
                table: "ExtraContacts",
                newName: "IX_ExtraContacts_StudentId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Trainers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainers",
                table: "Trainers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

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
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraContacts_Students_StudentId",
                table: "ExtraContacts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Trainers_TrainerId",
                table: "Groups",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_StudentId",
                table: "Payments",
                column: "StudentId",
                principalTable: "Students",
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
                name: "FK_Visits_Students_StudentId",
                table: "Visits",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
