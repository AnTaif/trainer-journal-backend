using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGroupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Halls_HallId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Groups_HallId",
                table: "Groups");

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
                name: "HallId",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Students",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "HexColor_Code",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Groups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("609db2d4-04f2-464d-b90d-6744b721ce2d"), "460a20f2-45d9-44ef-a61a-4803ba298881", "Admin", "ADMIN" },
                    { new Guid("a1d9c636-e746-4ea6-bcff-76eaec4790f2"), "89160391-fca7-4ec8-9bf7-7c9dd2b619f6", "User", "USER" },
                    { new Guid("cd812b3b-4e7f-4fd8-8c05-b4ce5cd5e467"), "c2dbebfc-1667-4ec8-b273-f6299b00d4cf", "Trainer", "TRAINER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("609db2d4-04f2-464d-b90d-6744b721ce2d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1d9c636-e746-4ea6-bcff-76eaec4790f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cd812b3b-4e7f-4fd8-8c05-b4ce5cd5e467"));

            migrationBuilder.DropColumn(
                name: "HexColor_Code",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HallId",
                table: "Groups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "IX_Groups_HallId",
                table: "Groups",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Halls_HallId",
                table: "Groups",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
