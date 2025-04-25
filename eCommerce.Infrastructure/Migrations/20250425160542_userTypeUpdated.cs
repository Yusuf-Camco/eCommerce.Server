using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userTypeUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03737617-808c-4844-a5de-fc8e20951f31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68d73b90-f6d3-42c8-9ef1-cd2657b0b4e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a263c331-bb42-4276-9063-494fe00aab71");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CheckOutArchives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d77eee4-9419-410d-bb0e-ac497dd4f897", null, "ADMIN", "ADMIN" },
                    { "aca836d9-dbfb-4e86-b498-a61fc2e6a4b6", null, "Credit Card", null },
                    { "e35a7081-f02c-4a37-8662-01067632205c", null, "USER", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d77eee4-9419-410d-bb0e-ac497dd4f897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aca836d9-dbfb-4e86-b498-a61fc2e6a4b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e35a7081-f02c-4a37-8662-01067632205c");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CheckOutArchives",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03737617-808c-4844-a5de-fc8e20951f31", null, "Credit Card", null },
                    { "68d73b90-f6d3-42c8-9ef1-cd2657b0b4e0", null, "USER", "USER" },
                    { "a263c331-bb42-4276-9063-494fe00aab71", null, "ADMIN", "ADMIN" }
                });
        }
    }
}
