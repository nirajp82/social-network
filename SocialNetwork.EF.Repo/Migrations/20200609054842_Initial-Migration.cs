using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    Title = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true),
                    Category = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Venue = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    FirstName = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    LastName = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    UserName = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    Passoword = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Salt = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUser_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("9c5f2164-facd-4fef-a3e4-bfef42720ff1"), "drinks", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 9, 1, 48, 41, 711, DateTimeKind.Local).AddTicks(4195), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pub" },
                    { new Guid("053965a9-4f13-482f-b06a-5e71255788ad"), "culture", "Paris", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(3840), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louvre" },
                    { new Guid("ccdf68da-e56c-4b05-a2a9-56c6bd4d62e4"), "culture", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4051), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Natural History Museum" },
                    { new Guid("b76788b0-3e51-456e-adc1-e9bb53b97baa"), "music", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4071), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "O2 Arena" },
                    { new Guid("3c72ec59-6023-4321-a822-041677152908"), "drinks", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4082), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Another pub" },
                    { new Guid("be2e1607-2bd1-41a1-ac0a-df36459b0eba"), "drinks", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4126), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yet another pub" },
                    { new Guid("c7b394b3-a682-4e41-a001-eefab9c6922d"), "drinks", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4136), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just another pub" },
                    { new Guid("dc2b8de6-5aba-4432-800c-0fc803c53216"), "music", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4146), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roundhouse Camden" },
                    { new Guid("5842a45f-686e-45d9-a3c5-701f017d6cae"), "travel", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4157), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Somewhere on the Thames" },
                    { new Guid("7f8159dc-049e-42b8-ad89-cdb224d9d9a7"), "film", "London", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 1, 48, 41, 715, DateTimeKind.Local).AddTicks(4171), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("74d180f0-99d1-43d8-bb11-5a939a822814"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("333d76e6-dbeb-46fe-b7ab-e6528f580a9f"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7baa55a9-6ce4-4197-838f-4525372a5f8e"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Values",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Value 101" },
                    { 2, "Value 201" },
                    { 3, "Value 301" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("4b6a94a3-68d3-4ba6-bd60-064744610395"), new Guid("74d180f0-99d1-43d8-bb11-5a939a822814"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JohnDoe@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("c5a07e9b-ab4b-4f20-92fa-4c3ebb24d8dc"), new Guid("333d76e6-dbeb-46fe-b7ab-e6528f580a9f"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane.Smith@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("2cd553f8-e054-48e0-b3cf-08106de464f5"), new Guid("7baa55a9-6ce4-4197-838f-4525372a5f8e"), "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruce.Lee@domain.com" });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_AppUserId",
                table: "IdentityUser",
                column: "AppUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
