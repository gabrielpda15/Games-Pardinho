using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesPardinho.Web.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League_Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationUser = table.Column<string>(maxLength: 50, nullable: true),
                    EditionUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationIp = table.Column<string>(maxLength: 50, nullable: true),
                    EditionIp = table.Column<string>(maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    EditionDate = table.Column<DateTime>(nullable: true),
                    SummonerName = table.Column<string>(maxLength: 16, nullable: false),
                    Elo = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "League_Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationUser = table.Column<string>(maxLength: 50, nullable: true),
                    EditionUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationIp = table.Column<string>(maxLength: 50, nullable: true),
                    EditionIp = table.Column<string>(maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    EditionDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League_Tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Security_Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationUser = table.Column<string>(maxLength: 50, nullable: true),
                    EditionUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationIp = table.Column<string>(maxLength: 50, nullable: true),
                    EditionIp = table.Column<string>(maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    EditionDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Url = table.Column<string>(maxLength: 100, nullable: false),
                    Target = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_Menu_Security_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Security_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Security_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    EditionDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Security_IdentityUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 80, nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    LeagueAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_IdentityUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_IdentityUser_League_Account_LeagueAccountId",
                        column: x => x.LeagueAccountId,
                        principalTable: "League_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "League_PlayerTournament",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League_PlayerTournament", x => new { x.PlayerId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_League_PlayerTournament_League_Account_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "League_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_League_PlayerTournament_League_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "League_Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "League_Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationUser = table.Column<string>(maxLength: 50, nullable: true),
                    EditionUser = table.Column<string>(maxLength: 50, nullable: true),
                    CreationIp = table.Column<string>(maxLength: 50, nullable: true),
                    EditionIp = table.Column<string>(maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    EditionDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    EloPoint = table.Column<int>(nullable: false),
                    Attempts = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_League_Team_League_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "League_Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Security_MenuRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    MenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_MenuRole", x => new { x.MenuId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Security_MenuRole_Security_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Security_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Security_MenuRole_Security_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Security_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_RoleClaim_Security_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Security_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_UserClaim_Security_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "Security_IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Security_UserLogin_Security_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "Security_IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RoleId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_UserRole", x => new { x.RoleId, x.UserId });
                    table.UniqueConstraint("AK_Security_UserRole_UserId_RoleId", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Security_UserRole_Security_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Security_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Security_UserRole_Security_Role_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Security_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Security_UserRole_Security_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "Security_IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security_UserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Security_UserToken_Security_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "Security_IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "League_TeamPlayer",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League_TeamPlayer", x => new { x.PlayerId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_League_TeamPlayer_League_Account_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "League_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_League_TeamPlayer_League_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "League_Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_League_PlayerTournament_TournamentId",
                table: "League_PlayerTournament",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_League_Team_TournamentId",
                table: "League_Team",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_League_TeamPlayer_TeamId",
                table: "League_TeamPlayer",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Security_IdentityUser_LeagueAccountId",
                table: "Security_IdentityUser",
                column: "LeagueAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Security_IdentityUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Security_IdentityUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Security_Menu_ParentId",
                table: "Security_Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Security_MenuRole_RoleId",
                table: "Security_MenuRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Security_Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Security_RoleClaim_RoleId",
                table: "Security_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Security_UserClaim_UserId",
                table: "Security_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Security_UserLogin_UserId",
                table: "Security_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Security_UserRole_RoleId1",
                table: "Security_UserRole",
                column: "RoleId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "League_PlayerTournament");

            migrationBuilder.DropTable(
                name: "League_TeamPlayer");

            migrationBuilder.DropTable(
                name: "Security_MenuRole");

            migrationBuilder.DropTable(
                name: "Security_RoleClaim");

            migrationBuilder.DropTable(
                name: "Security_UserClaim");

            migrationBuilder.DropTable(
                name: "Security_UserLogin");

            migrationBuilder.DropTable(
                name: "Security_UserRole");

            migrationBuilder.DropTable(
                name: "Security_UserToken");

            migrationBuilder.DropTable(
                name: "League_Team");

            migrationBuilder.DropTable(
                name: "Security_Menu");

            migrationBuilder.DropTable(
                name: "Security_Role");

            migrationBuilder.DropTable(
                name: "Security_IdentityUser");

            migrationBuilder.DropTable(
                name: "League_Tournament");

            migrationBuilder.DropTable(
                name: "League_Account");
        }
    }
}
