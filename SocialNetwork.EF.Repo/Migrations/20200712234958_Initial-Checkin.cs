using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class InitialCheckin : Migration
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
                    { new Guid("4f74da04-2b1a-4eda-8a0a-2bfa6e127402"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(8895), new DateTime(2020, 5, 12, 19, 49, 57, 557, DateTimeKind.Local).AddTicks(4582), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(9664), "Pub" },
                    { new Guid("fcca52bb-630b-41f7-bc08-7ef54e0059ab"), "culture", "Paris", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(598), new DateTime(2020, 6, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3758), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(673), "Louvre" },
                    { new Guid("87cea642-4afb-4cc9-8b90-1e7be653fa4a"), "culture", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(705), new DateTime(2020, 8, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3931), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(712), "Natural History Museum" },
                    { new Guid("65dda113-2519-4a4a-a811-3d444c966205"), "music", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(720), new DateTime(2020, 9, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3946), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(726), "O2 Arena" },
                    { new Guid("e223904d-9518-43af-b20b-f929e7cc7f84"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(756), new DateTime(2020, 10, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3955), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(762), "Another pub" },
                    { new Guid("f1e2ef5c-2576-4cf1-9e93-dcf8213f5a3e"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(771), new DateTime(2020, 11, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3970), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(778), "Yet another pub" },
                    { new Guid("326635b0-4f5c-416e-8183-876cb7984a89"), "drinks", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(785), new DateTime(2020, 12, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3979), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(791), "Just another pub" },
                    { new Guid("82aa4561-93d2-4f05-864c-4a2510c2fa13"), "music", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(798), new DateTime(2021, 1, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3988), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(805), "Roundhouse Camden" },
                    { new Guid("b478363b-94c7-4816-9095-a96fd89c160f"), "travel", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(813), new DateTime(2021, 2, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(3997), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(820), "Somewhere on the Thames" },
                    { new Guid("7ca5cc71-6bdc-40ae-bc41-1157fe724f55"), "film", "London", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(828), new DateTime(2021, 3, 12, 19, 49, 57, 561, DateTimeKind.Local).AddTicks(4009), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 562, DateTimeKind.Local).AddTicks(834), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Bio", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d"), null, "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(111), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(286) },
                    { new Guid("0f463d2c-6227-4ea8-abad-304669ee661a"), null, "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(362), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(369) },
                    { new Guid("581e4e4d-90ed-4afa-8799-85706984256e"), null, "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(395), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(401) },
                    { new Guid("4d2650c0-3387-47f5-a48b-812997ccf24c"), null, "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(408), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 566, DateTimeKind.Local).AddTicks(413) }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "RefreshToken", "RefreshTokenExpiry", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("d65d1fc3-b593-4e6e-b47f-ab4985fb8584"), new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d"), "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5247), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", null, null, "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5401), "JohnDoe@domain.com" },
                    { new Guid("9e68581a-4588-41d9-89b1-c54f8be60c20"), new Guid("0f463d2c-6227-4ea8-abad-304669ee661a"), "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5789), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", null, null, "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5801), "Jane.Smith@domain.com" },
                    { new Guid("9c59f24a-97c0-4eee-8088-9d842cf236a7"), new Guid("581e4e4d-90ed-4afa-8799-85706984256e"), "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5814), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", null, null, "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5821), "Bruce.Lee@domain.com" },
                    { new Guid("70d318a0-a392-4759-95b3-7b4e0f062d1b"), new Guid("4d2650c0-3387-47f5-a48b-812997ccf24c"), "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5832), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", null, null, "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 7, 12, 19, 49, 57, 567, DateTimeKind.Local).AddTicks(5839), "string" }
                });

            migrationBuilder.InsertData(
                table: "UserFollower",
                columns: new[] { "UserId", "FollowerId" },
                values: new object[,]
                {
                    { new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d"), new Guid("0f463d2c-6227-4ea8-abad-304669ee661a") },
                    { new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d"), new Guid("581e4e4d-90ed-4afa-8799-85706984256e") },
                    { new Guid("581e4e4d-90ed-4afa-8799-85706984256e"), new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d") },
                    { new Guid("0f463d2c-6227-4ea8-abad-304669ee661a"), new Guid("581e4e4d-90ed-4afa-8799-85706984256e") },
                    { new Guid("581e4e4d-90ed-4afa-8799-85706984256e"), new Guid("0f463d2c-6227-4ea8-abad-304669ee661a") },
                    { new Guid("9cd36689-42c2-425e-8eb1-b52e68a2ea1d"), new Guid("4d2650c0-3387-47f5-a48b-812997ccf24c") },
                    { new Guid("0f463d2c-6227-4ea8-abad-304669ee661a"), new Guid("4d2650c0-3387-47f5-a48b-812997ccf24c") },
                    { new Guid("581e4e4d-90ed-4afa-8799-85706984256e"), new Guid("4d2650c0-3387-47f5-a48b-812997ccf24c") }
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
