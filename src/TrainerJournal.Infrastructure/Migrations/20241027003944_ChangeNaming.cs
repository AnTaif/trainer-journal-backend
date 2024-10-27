using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainerJournal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "LastAikidoGradeDate",
                table: "Students",
                newName: "KyuUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "AikidoGrade",
                table: "Students",
                newName: "Kyu");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("04237cfc-e5f5-4e25-b0f6-f3785ffc7e08"), "7860ffd9-536b-41c4-84cf-80cf480ba521", "Trainer", "TRAINER" },
                    { new Guid("2a3e9d0a-a92d-402c-b2e5-a89d57c17df3"), "66c829c3-d66b-4306-beb8-851fcf299af5", "Admin", "ADMIN" },
                    { new Guid("c4844ef0-032f-4997-ad91-280f555cb60e"), "37c37686-d66a-4ed3-a866-6ff43c456e52", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04237cfc-e5f5-4e25-b0f6-f3785ffc7e08"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a3e9d0a-a92d-402c-b2e5-a89d57c17df3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4844ef0-032f-4997-ad91-280f555cb60e"));

            migrationBuilder.RenameColumn(
                name: "KyuUpdatedAt",
                table: "Students",
                newName: "LastAikidoGradeDate");

            migrationBuilder.RenameColumn(
                name: "Kyu",
                table: "Students",
                newName: "AikidoGrade");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4bffe4c8-0140-4b1b-af78-9c1b1051bf4c"), "5dcb20cb-d72d-493f-870b-cc658aac7a9e", "User", "USER" },
                    { new Guid("5aaf45b3-b866-429e-8346-03330cceaa46"), "fb875391-a8da-4596-9c65-eeea39bd8e93", "Trainer", "TRAINER" },
                    { new Guid("f615cd30-8802-4af9-a867-eff4df9acaed"), "92b109f2-d8e9-4dd2-885b-dd5ff2fe9e16", "Admin", "ADMIN" }
                });
        }
    }
}
