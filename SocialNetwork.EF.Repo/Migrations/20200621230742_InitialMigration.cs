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
                    Email = table.Column<string>(type: "VARCHAR(24)", maxLength: 24, nullable: true),
                    Bio = table.Column<string>(type: "VARCHAR(240)", maxLength: 240, nullable: true)
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
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsMainPhoto = table.Column<bool>(nullable: false),
                    ActualFileName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    CloudFileName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    ContentType = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Length = table.Column<long>(nullable: false),
                    UploadedDate = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_AppUser_AppUserId",
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
                    { new Guid("eb1f7fd9-a352-4907-862a-8b0f7e975853"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(8543), new DateTime(2020, 4, 21, 19, 7, 41, 609, DateTimeKind.Local).AddTicks(5494), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(9734), "Pub" },
                    { new Guid("8f967057-de73-49ac-89e6-8e2b5c64b840"), "film", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1761), new DateTime(2021, 2, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(747), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1768), "Cinema" },
                    { new Guid("3f3434ba-268a-4a67-987e-52a76f96c130"), "music", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1729), new DateTime(2020, 12, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(717), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1736), "Roundhouse Camden" },
                    { new Guid("3c738613-6312-4d62-bf14-93def26593e6"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1701), new DateTime(2020, 11, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(704), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1708), "Just another pub" },
                    { new Guid("39427a54-8315-447b-9bb2-990db9ce6afa"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1685), new DateTime(2020, 10, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(692), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1692), "Yet another pub" },
                    { new Guid("13ed81c7-6b31-411f-b945-e529d0fc7c69"), "travel", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1745), new DateTime(2021, 1, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(730), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1752), "Somewhere on the Thames" },
                    { new Guid("d5b6d235-96b8-4020-9b33-5802d245df15"), "music", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1639), new DateTime(2020, 8, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(656), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1651), "O2 Arena" },
                    { new Guid("6fb3789b-5a76-4812-9717-5c28b5b9a33c"), "culture", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1611), new DateTime(2020, 7, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(632), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1624), "Natural History Museum" },
                    { new Guid("efed3e18-0910-4bd8-aa70-17fce47c3cba"), "culture", "Paris", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1417), new DateTime(2020, 5, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(377), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1533), "Louvre" },
                    { new Guid("f4676b1a-d1c3-43c3-b737-781370d66144"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1666), new DateTime(2020, 9, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(670), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1674), "Another pub" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Bio", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("b0c2f35c-77ba-4b69-9c74-97d02f3aa27c"), null, "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7630), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7837) },
                    { new Guid("78151b38-ad7f-4e27-b30d-3d8690c73237"), null, "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7949), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7962) },
                    { new Guid("551e9172-9202-4985-9865-4df2f53fcf2b"), null, "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7982), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(7990) },
                    { new Guid("b4872b74-272e-42f2-9631-e43baec8a059"), null, "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(8003), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 619, DateTimeKind.Local).AddTicks(8015) }
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
                    { new Guid("aaf7dec0-ce9b-4e49-afe4-498376e0e307"), new Guid("b0c2f35c-77ba-4b69-9c74-97d02f3aa27c"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(586), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(873), "JohnDoe@domain.com" },
                    { new Guid("b1115bf2-5639-4f48-a710-f02933c82c4d"), new Guid("78151b38-ad7f-4e27-b30d-3d8690c73237"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1585), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1598), "Jane.Smith@domain.com" },
                    { new Guid("de0ce645-0350-43a4-9ef3-7676dac4824e"), new Guid("551e9172-9202-4985-9865-4df2f53fcf2b"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1613), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1620), "Bruce.Lee@domain.com" },
                    { new Guid("983d5598-0960-43a0-9f7d-a9c3f4e2e751"), new Guid("b4872b74-272e-42f2-9631-e43baec8a059"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1631), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1638), "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_AppUserId",
                table: "IdentityUser",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_AppUserId",
                table: "Photo",
                column: "AppUserId");

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
                name: "Photo");

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
