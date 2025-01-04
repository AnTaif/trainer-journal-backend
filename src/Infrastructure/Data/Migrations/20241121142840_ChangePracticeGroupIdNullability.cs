using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePracticeGroupIdNullability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6afef4ca-abee-4841-ae6c-0cecf638468f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7350aec2-685e-480c-b23b-e23c8767291c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af047d79-eff5-44b9-b09a-a938d8072e2f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Practices",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0df66019-bb0d-40e6-bc0a-7a24cea21c21"), "0f5c4f0b-8ebb-4bec-9cc4-3a93491d752e", "Admin", "ADMIN" },
                    { new Guid("33f9445a-37d1-4f9d-8b08-1fb62a035aa5"), "d52910a2-645b-4ddb-8c01-f368852621a8", "Trainer", "TRAINER" },
                    { new Guid("94fa17a7-0473-4c86-9914-3f55bc07abdd"), "05b90da8-78d5-46f0-9eb9-3c9c3fb49946", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0df66019-bb0d-40e6-bc0a-7a24cea21c21"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33f9445a-37d1-4f9d-8b08-1fb62a035aa5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("94fa17a7-0473-4c86-9914-3f55bc07abdd"));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "Practices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6afef4ca-abee-4841-ae6c-0cecf638468f"), "90bd2d45-06bc-447b-a961-944a381e1729", "User", "USER" },
                    { new Guid("7350aec2-685e-480c-b23b-e23c8767291c"), "10c115b2-1846-4826-8e81-c0bb8af3f7fd", "Admin", "ADMIN" },
                    { new Guid("af047d79-eff5-44b9-b09a-a938d8072e2f"), "432e0f98-ea90-424c-9d38-e21351d7a6ef", "Trainer", "TRAINER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Groups_GroupId",
                table: "Practices",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
