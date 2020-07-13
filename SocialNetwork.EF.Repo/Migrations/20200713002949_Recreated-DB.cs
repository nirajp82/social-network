using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class RecreatedDB : Migration
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
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(type: "VARCHAR(240)", maxLength: 240, nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_AppUser_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    RefreshToken = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "UserFollower",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    FollowerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollower", x => new { x.UserId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_UserFollower_AppUser_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFollower_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("9411f42d-592a-4b01-bee8-db08f9473a48"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1720), new DateTime(2020, 5, 12, 20, 29, 49, 54, DateTimeKind.Local).AddTicks(6355), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1805), "Pub" },
                    { new Guid("8418c88c-f351-4816-b36a-c2bb55cf0825"), "drinks", "Fairfax", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2051), new DateTime(2021, 8, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(407), "Activity 13 months in future", "Future Activity 13", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2057), "Disco Place" },
                    { new Guid("f75728db-f94a-4c7e-be7f-b6fa38354872"), "music", "Seattle", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2038), new DateTime(2021, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(399), "Activity 12 months in future", "Future Activity 12", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2043), "Party Place" },
                    { new Guid("c10a7210-3887-4c1c-b584-df6f78dee370"), "music", "RVA", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2024), new DateTime(2021, 6, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(391), "Activity 11 months in future", "Future Activity 11", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2030), "Bollywood Music" },
                    { new Guid("e102f5ab-0ded-4a3d-96a0-7bafe78ab476"), "music", "Glen Allen", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2010), new DateTime(2021, 5, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(382), "Activity 10 months in future", "Future Activity 10", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2015), "Party Place" },
                    { new Guid("bcdae06a-2c5e-4019-bdab-b59893df8a08"), "drinks", "Richmond", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1996), new DateTime(2021, 4, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(374), "Activity 9 months in future", "Future Activity 9", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(2002), "Pub" },
                    { new Guid("5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3"), "travel", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1968), new DateTime(2021, 2, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(355), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1974), "Somewhere on the Thames" },
                    { new Guid("76a7f1e7-f9ee-4df2-9268-3d7bd4a15578"), "film", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1982), new DateTime(2021, 3, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(366), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1988), "Cinema" },
                    { new Guid("7d9ab25f-2ca9-42fa-b2d9-29d40892a26b"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1933), new DateTime(2020, 12, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(339), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1939), "Just another pub" },
                    { new Guid("753d5b55-ff88-4a28-a92c-364c3b68736c"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1919), new DateTime(2020, 11, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(330), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1925), "Yet another pub" },
                    { new Guid("12b96b10-4fba-4b85-8804-b7172f0eaff1"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1905), new DateTime(2020, 10, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(315), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1911), "Another pub" },
                    { new Guid("d3fe056e-2fbd-435b-93f8-79a39142b530"), "music", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1892), new DateTime(2020, 9, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(307), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1898), "O2 Arena" },
                    { new Guid("d48ac8d3-7ff4-46c8-b652-5d87afbfd1f7"), "culture", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1878), new DateTime(2020, 8, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(297), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1885), "Natural History Museum" },
                    { new Guid("f74a0ce5-1bfb-4b94-b0c5-663629ec8588"), "culture", "Paris", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1863), new DateTime(2020, 6, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(189), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1870), "Louvre" },
                    { new Guid("00e6b54a-27bf-4e0e-897f-0773c8c2ada5"), "music", "London", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1947), new DateTime(2021, 1, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(346), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(1952), "Roundhouse Camden" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Bio", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), null, "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5567), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5574) },
                    { new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), null, "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 44, DateTimeKind.Local).AddTicks(723), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(4200) },
                    { new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), null, "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5431), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5528) },
                    { new Guid("5249a8c8-528e-4090-b404-40d5123e4af4"), null, "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5582), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 47, DateTimeKind.Local).AddTicks(5588) }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "RefreshToken", "RefreshTokenExpiry", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("29d18e18-3b07-452d-9caa-7a14f2750d14"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5254), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", null, null, "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5394), "JohnDoe@domain.com" },
                    { new Guid("658aaf4c-6c7b-435d-8194-e20b57ff7f0c"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5740), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", null, null, "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5749), "Jane.Smith@domain.com" },
                    { new Guid("53926a03-90a8-4eeb-b3c4-5c541f9f66b6"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5763), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", null, null, "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5770), "Bruce.Lee@domain.com" },
                    { new Guid("fd7a3c5d-c9bd-4a18-8a38-672b2b7f5f96"), new Guid("5249a8c8-528e-4090-b404-40d5123e4af4"), "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5781), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", null, null, "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 7, 12, 20, 29, 49, 51, DateTimeKind.Local).AddTicks(5788), "string" }
                });

            migrationBuilder.InsertData(
                table: "UserActivity",
                columns: new[] { "ActivityId", "AppUserId", "DateJoined", "IsHost" },
                values: new object[,]
                {
                    { new Guid("bcdae06a-2c5e-4019-bdab-b59893df8a08"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2021, 4, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(374), false },
                    { new Guid("76a7f1e7-f9ee-4df2-9268-3d7bd4a15578"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2021, 3, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(366), false },
                    { new Guid("7d9ab25f-2ca9-42fa-b2d9-29d40892a26b"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2020, 12, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(339), false },
                    { new Guid("12b96b10-4fba-4b85-8804-b7172f0eaff1"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2020, 10, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(315), false },
                    { new Guid("d48ac8d3-7ff4-46c8-b652-5d87afbfd1f7"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2020, 8, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(297), false },
                    { new Guid("8418c88c-f351-4816-b36a-c2bb55cf0825"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2021, 8, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(407), false },
                    { new Guid("bcdae06a-2c5e-4019-bdab-b59893df8a08"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2021, 4, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(374), false },
                    { new Guid("76a7f1e7-f9ee-4df2-9268-3d7bd4a15578"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2021, 3, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(366), false },
                    { new Guid("5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2021, 2, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(355), false },
                    { new Guid("00e6b54a-27bf-4e0e-897f-0773c8c2ada5"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2021, 1, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(346), false },
                    { new Guid("753d5b55-ff88-4a28-a92c-364c3b68736c"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 11, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(330), false },
                    { new Guid("12b96b10-4fba-4b85-8804-b7172f0eaff1"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 10, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(315), false },
                    { new Guid("8418c88c-f351-4816-b36a-c2bb55cf0825"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new DateTime(2021, 8, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(407), false },
                    { new Guid("d3fe056e-2fbd-435b-93f8-79a39142b530"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 9, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(307), false },
                    { new Guid("7d9ab25f-2ca9-42fa-b2d9-29d40892a26b"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 12, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(339), false },
                    { new Guid("9411f42d-592a-4b01-bee8-db08f9473a48"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 5, 14, 20, 29, 49, 54, DateTimeKind.Local).AddTicks(6355), false },
                    { new Guid("9411f42d-592a-4b01-bee8-db08f9473a48"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 5, 12, 20, 29, 49, 54, DateTimeKind.Local).AddTicks(6355), true },
                    { new Guid("f74a0ce5-1bfb-4b94-b0c5-663629ec8588"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 6, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(189), true },
                    { new Guid("d48ac8d3-7ff4-46c8-b652-5d87afbfd1f7"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 8, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(297), true },
                    { new Guid("d3fe056e-2fbd-435b-93f8-79a39142b530"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 9, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(307), true },
                    { new Guid("d48ac8d3-7ff4-46c8-b652-5d87afbfd1f7"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new DateTime(2020, 8, 14, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(297), false },
                    { new Guid("753d5b55-ff88-4a28-a92c-364c3b68736c"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 11, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(330), true },
                    { new Guid("7d9ab25f-2ca9-42fa-b2d9-29d40892a26b"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 12, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(339), true },
                    { new Guid("00e6b54a-27bf-4e0e-897f-0773c8c2ada5"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 1, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(346), true },
                    { new Guid("12b96b10-4fba-4b85-8804-b7172f0eaff1"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2020, 10, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(315), true },
                    { new Guid("5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 2, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(355), true },
                    { new Guid("76a7f1e7-f9ee-4df2-9268-3d7bd4a15578"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 3, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(366), true },
                    { new Guid("bcdae06a-2c5e-4019-bdab-b59893df8a08"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 4, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(374), true },
                    { new Guid("e102f5ab-0ded-4a3d-96a0-7bafe78ab476"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 5, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(382), true },
                    { new Guid("c10a7210-3887-4c1c-b584-df6f78dee370"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 6, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(391), true },
                    { new Guid("f75728db-f94a-4c7e-be7f-b6fa38354872"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 7, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(399), true },
                    { new Guid("8418c88c-f351-4816-b36a-c2bb55cf0825"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new DateTime(2021, 8, 12, 20, 29, 49, 55, DateTimeKind.Local).AddTicks(407), true }
                });

            migrationBuilder.InsertData(
                table: "UserFollower",
                columns: new[] { "UserId", "FollowerId" },
                values: new object[,]
                {
                    { new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new Guid("5249a8c8-528e-4090-b404-40d5123e4af4") },
                    { new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new Guid("5249a8c8-528e-4090-b404-40d5123e4af4") },
                    { new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new Guid("5249a8c8-528e-4090-b404-40d5123e4af4") },
                    { new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1") },
                    { new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936") },
                    { new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a") },
                    { new Guid("327987e6-fe50-4ebc-9729-fa87348857c1"), new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936") },
                    { new Guid("5249a8c8-528e-4090-b404-40d5123e4af4"), new Guid("327987e6-fe50-4ebc-9729-fa87348857c1") },
                    { new Guid("08d170bc-a29c-4591-a3e8-abf513d5f936"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a") },
                    { new Guid("5249a8c8-528e-4090-b404-40d5123e4af4"), new Guid("cf94be86-baa2-46e2-9efe-a212ca53dc3a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ActivityId",
                table: "Comment",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "UserActivity");

            migrationBuilder.DropTable(
                name: "UserFollower");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
