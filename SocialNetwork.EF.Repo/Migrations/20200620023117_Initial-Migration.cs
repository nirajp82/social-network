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
                    { new Guid("7613990e-c668-453e-9f8d-e16124f30e46"), "drinks", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(1234), new DateTime(2020, 4, 19, 22, 31, 16, 526, DateTimeKind.Local).AddTicks(6625), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(1842), "Pub" },
                    { new Guid("889ff5af-1a02-4c9c-b231-73b1267be854"), "film", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2657), new DateTime(2021, 2, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7782), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2661), "Cinema" },
                    { new Guid("476844c5-46f5-4b95-958f-2e8199286f77"), "music", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2641), new DateTime(2020, 12, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7769), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2645), "Roundhouse Camden" },
                    { new Guid("bda0114e-2e66-4972-9255-ba9231aa5940"), "drinks", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2617), new DateTime(2020, 11, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7763), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2621), "Just another pub" },
                    { new Guid("49bedc85-4de2-434b-9252-9be64feeddce"), "drinks", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2608), new DateTime(2020, 10, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7756), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2612), "Yet another pub" },
                    { new Guid("0589e668-24a8-41b9-b5bc-17812a13b6c7"), "travel", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2649), new DateTime(2021, 1, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7774), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2653), "Somewhere on the Thames" },
                    { new Guid("8547c80a-09e1-48c3-aabe-8f1d074be38d"), "music", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2592), new DateTime(2020, 8, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7650), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2595), "O2 Arena" },
                    { new Guid("28aa731b-5e7a-4516-871e-a10a6ca6b5c2"), "culture", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2582), new DateTime(2020, 7, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7639), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2586), "Natural History Museum" },
                    { new Guid("44f145ad-ad2a-4031-a5b3-d253e0ed6480"), "culture", "Paris", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2508), new DateTime(2020, 5, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7514), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2560), "Louvre" },
                    { new Guid("d481ad8e-757d-4c7d-bd28-3248c05ff9a4"), "drinks", "London", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2600), new DateTime(2020, 9, 19, 22, 31, 16, 529, DateTimeKind.Local).AddTicks(7655), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 530, DateTimeKind.Local).AddTicks(2604), "Another pub" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("d377a106-380f-48e3-a049-e434943b2e13"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2843), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2897) },
                    { new Guid("04dc3fab-d529-406d-a542-771c16250615"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2942), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2947) },
                    { new Guid("3885f8f6-72eb-4bc0-bb43-443564f0df3b"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2954), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2958) },
                    { new Guid("33f1bce9-29e0-46c3-bd49-2302a345931c"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2963), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 531, DateTimeKind.Local).AddTicks(2966) }
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
                    { new Guid("68741c21-35bf-4564-aa03-c35293ca972a"), new Guid("d377a106-380f-48e3-a049-e434943b2e13"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3320), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3395), "JohnDoe@domain.com" },
                    { new Guid("b87e8320-ac39-4dcc-adc0-0963ae4f25f9"), new Guid("04dc3fab-d529-406d-a542-771c16250615"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3684), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3692), "Jane.Smith@domain.com" },
                    { new Guid("bd6ad8bb-7f52-4edd-9589-70e4bdace0d5"), new Guid("3885f8f6-72eb-4bc0-bb43-443564f0df3b"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3701), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3705), "Bruce.Lee@domain.com" },
                    { new Guid("a136495b-bb12-4894-8947-24d609f6d9fa"), new Guid("33f1bce9-29e0-46c3-bd49-2302a345931c"), "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3712), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 19, 22, 31, 16, 532, DateTimeKind.Local).AddTicks(3715), "string" }
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
