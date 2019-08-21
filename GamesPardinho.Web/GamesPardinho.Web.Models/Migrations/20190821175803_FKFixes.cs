using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesPardinho.Web.Models.Migrations
{
    public partial class FKFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Security_IdentityUser_League_Account_LeagueAccountId",
                table: "Security_IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Security_IdentityUser_LeagueAccountId",
                table: "Security_IdentityUser");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueAccountId",
                table: "Security_IdentityUser",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Security_IdentityUser_LeagueAccountId",
                table: "Security_IdentityUser",
                column: "LeagueAccountId",
                unique: true,
                filter: "[LeagueAccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Security_IdentityUser_League_Account_LeagueAccountId",
                table: "Security_IdentityUser",
                column: "LeagueAccountId",
                principalTable: "League_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Security_IdentityUser_League_Account_LeagueAccountId",
                table: "Security_IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Security_IdentityUser_LeagueAccountId",
                table: "Security_IdentityUser");

            migrationBuilder.AlterColumn<int>(
                name: "LeagueAccountId",
                table: "Security_IdentityUser",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Security_IdentityUser_LeagueAccountId",
                table: "Security_IdentityUser",
                column: "LeagueAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Security_IdentityUser_League_Account_LeagueAccountId",
                table: "Security_IdentityUser",
                column: "LeagueAccountId",
                principalTable: "League_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
