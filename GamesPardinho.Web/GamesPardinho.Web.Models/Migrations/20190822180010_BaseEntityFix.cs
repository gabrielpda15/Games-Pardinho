using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesPardinho.Web.Models.Migrations
{
    public partial class BaseEntityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_UserToken",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_UserToken",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_UserToken",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "Security_UserToken",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_UserToken",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_UserToken",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_UserLogin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_UserLogin",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_UserLogin",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "Security_UserLogin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_UserLogin",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_UserLogin",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_UserClaim",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_UserClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_UserClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "Security_UserClaim",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_UserClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_UserClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_RoleClaim",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_RoleClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_RoleClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "Security_RoleClaim",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_RoleClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_RoleClaim",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_Role",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_Role",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_Role",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_Role",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_Role",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Security_IdentityUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationIp",
                table: "Security_IdentityUser",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Security_IdentityUser",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "Security_IdentityUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionIp",
                table: "Security_IdentityUser",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionUser",
                table: "Security_IdentityUser",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_UserToken");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_UserLogin");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_UserClaim");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_RoleClaim");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_Role");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_Role");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_Role");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_Role");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_Role");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Security_IdentityUser");

            migrationBuilder.DropColumn(
                name: "CreationIp",
                table: "Security_IdentityUser");

            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Security_IdentityUser");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "Security_IdentityUser");

            migrationBuilder.DropColumn(
                name: "EditionIp",
                table: "Security_IdentityUser");

            migrationBuilder.DropColumn(
                name: "EditionUser",
                table: "Security_IdentityUser");
        }
    }
}
