using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class CreateActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    Category = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(type: "VARCHAR(1)", nullable: true),
                    Venue = table.Column<string>(type: "VARCHAR(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
