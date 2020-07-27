using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class SeedMethodAndProfilePic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e571", "4a0f5d04-ca80-473e-a299-c4148b193802", "Administrator", "Admin" },
                    { "a18be9c0-aa65-4af8-bd17-00bd9344e572", "66a19a82-55ef-4aad-a58c-c23af1751625", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImage", "Score", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", 0, "9b988573-1d38-4f6a-9210-0b032b8cb71e", "tm_dev_team@tm.com", true, null, null, false, null, "TM_DEV_TEAM@TM.COM", "TM_DEV_TEAM@TM.COM", "AQAAAAEAACcQAAAAEDlLeFFTOOfMFqbKXuZ/mBBVlZzXV6wRgthC/zwoFhgUL/d1/ePpZMwniP8CG0TV7Q==", null, false, null, 0, "", false, "tm_dev_team@tm.com" });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "BadgesId", "Name", "NecessaryScore" },
                values: new object[,]
                {
                    { 1, "Beginner", 500 },
                    { 2, "Advanced", 25000 },
                    { 3, "Expert", 50000 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e571", "UserRoles" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e572");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "a18be9c0-aa65-4af8-bd17-00bd9344e575", "a18be9c0-aa65-4af8-bd17-00bd9344e571" });

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "BadgesId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "BadgesId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Badges",
                keyColumn: "BadgesId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e571");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");
        }
    }
}
