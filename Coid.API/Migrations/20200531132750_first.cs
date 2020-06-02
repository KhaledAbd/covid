using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coid.API.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Confirmed = table.Column<string>(nullable: true),
                    Deaths = table.Column<int>(nullable: false),
                    Recovered = table.Column<int>(nullable: false),
                    Active = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    KnownAs = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    passwordHash = table.Column<byte[]>(nullable: true),
                    passwordSalt = table.Column<byte[]>(nullable: true),
                    typePerson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    IsCertificated = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vidoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vidoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vidoes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vidoes_UserId",
                table: "Vidoes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Corons");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Vidoes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
