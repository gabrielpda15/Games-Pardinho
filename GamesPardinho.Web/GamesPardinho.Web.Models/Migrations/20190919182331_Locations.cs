using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesPardinho.Web.Models.Migrations
{
    public partial class Locations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SummonerId",
                table: "League_Account",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Location_Country",
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
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    Code = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location_Region",
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
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Region_Location_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Location_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location_City",
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
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_City_Location_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Location_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Location_City_Location_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Location_Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_City_CountryId",
                table: "Location_City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_City_RegionId",
                table: "Location_City",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Region_CountryId",
                table: "Location_Region",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location_City");

            migrationBuilder.DropTable(
                name: "Location_Region");

            migrationBuilder.DropTable(
                name: "Location_Country");

            migrationBuilder.DropColumn(
                name: "SummonerId",
                table: "League_Account");
        }
    }
}
