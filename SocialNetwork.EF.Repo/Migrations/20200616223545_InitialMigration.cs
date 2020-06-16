using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
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
                    table.PrimaryKey("PK_Activity", x => x.Id);
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
                name: "Value",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Value", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "UserActivity",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    IsHost = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivity", x => new { x.ActivityId, x.AppUserId });
                    table.ForeignKey(
                        name: "FK_UserActivity_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActivity_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("cc6e5f5d-efd4-4ae7-9c7b-1446f489661b"), "drinks", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(1185), new DateTime(2020, 4, 16, 18, 35, 45, 156, DateTimeKind.Local).AddTicks(2315), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(1737), "Pub" },
                    { new Guid("11e7a04e-c7fc-4839-ac65-6d2de8e654c1"), "film", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2480), new DateTime(2021, 2, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7922), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2483), "Cinema" },
                    { new Guid("85f19760-f352-4c4d-8260-e5da6767d68b"), "music", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2464), new DateTime(2020, 12, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7911), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2467), "Roundhouse Camden" },
                    { new Guid("ea59435f-cf39-43e2-b918-66260222fa41"), "drinks", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2444), new DateTime(2020, 11, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7907), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2447), "Just another pub" },
                    { new Guid("f91fdf99-394f-41ba-8c0b-e40e207f6be1"), "drinks", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2436), new DateTime(2020, 10, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7903), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2439), "Yet another pub" },
                    { new Guid("da1b49ed-2e3d-4847-a1cb-fc96b9eb3f4b"), "travel", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2472), new DateTime(2021, 1, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7916), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2475), "Somewhere on the Thames" },
                    { new Guid("6ef7e062-02df-45dd-a30e-e626887cea7c"), "music", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2420), new DateTime(2020, 8, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7889), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2423), "O2 Arena" },
                    { new Guid("30bee69c-31cd-4479-9bf0-32cd5fc2085f"), "culture", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2411), new DateTime(2020, 7, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7880), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2415), "Natural History Museum" },
                    { new Guid("d9555f29-247f-4890-8047-e77bb5accafe"), "culture", "Paris", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2340), new DateTime(2020, 5, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7764), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2391), "Louvre" },
                    { new Guid("0a8d55fa-8f65-4a29-8f30-0dbbb118c023"), "drinks", "London", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2428), new DateTime(2020, 9, 16, 18, 35, 45, 158, DateTimeKind.Local).AddTicks(7894), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 159, DateTimeKind.Local).AddTicks(2431), "Another pub" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f2d026c6-f66e-47e3-989e-2ba7f68ec838"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6524), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6615) },
                    { new Guid("8d6e19fd-9519-4404-9d4c-5182d07fba81"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6677), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6682) },
                    { new Guid("b11dcf8d-ea63-48f4-a0ec-d38acd60abc6"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6687), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6691) },
                    { new Guid("2f664e0b-b1ac-4b08-aea0-02f88a7f796e"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6695), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 160, DateTimeKind.Local).AddTicks(6698) }
                });

            migrationBuilder.InsertData(
                table: "Value",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Value 201" },
                    { 1, "Value 101" },
                    { 3, "Value 301" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("cac5fbfc-ebd2-4125-a238-3ddc59587b0f"), new Guid("f2d026c6-f66e-47e3-989e-2ba7f68ec838"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7359), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7419), "JohnDoe@domain.com" },
                    { new Guid("34a153f9-f329-4fde-b6a0-4b9d40de3d6f"), new Guid("8d6e19fd-9519-4404-9d4c-5182d07fba81"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7647), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7654), "Jane.Smith@domain.com" },
                    { new Guid("53213700-7245-469c-a8ec-13d63ac2ed0f"), new Guid("b11dcf8d-ea63-48f4-a0ec-d38acd60abc6"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7663), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7666), "Bruce.Lee@domain.com" },
                    { new Guid("a2feb703-e506-4493-93be-3c4df30d7f84"), new Guid("2f664e0b-b1ac-4b08-aea0-02f88a7f796e"), "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7673), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 16, 18, 35, 45, 161, DateTimeKind.Local).AddTicks(7676), "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_AppUserId",
                table: "IdentityUser",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_AppUserId",
                table: "UserActivity",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "UserActivity");

            migrationBuilder.DropTable(
                name: "Value");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
