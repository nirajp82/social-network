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
                    { new Guid("7af11db1-b7f1-43e6-855d-baee9d0d1e47"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(9652), new DateTime(2020, 4, 9, 1, 59, 54, 732, DateTimeKind.Local).AddTicks(8595), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(517), "Pub" },
                    { new Guid("8bf4d332-c011-4d21-852f-b6d225936c0a"), "culture", "Paris", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1245), new DateTime(2020, 5, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(5850), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1311), "Louvre" },
                    { new Guid("a3d4ab26-de38-446a-a4a6-04a341e7a33f"), "culture", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1333), new DateTime(2020, 7, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(5976), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1337), "Natural History Museum" },
                    { new Guid("bfda5fc8-9266-4cec-b8f0-808b697f14d4"), "music", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1342), new DateTime(2020, 8, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(5985), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1346), "O2 Arena" },
                    { new Guid("c17dd6f0-0df7-4a46-a49c-a32fe5825042"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1350), new DateTime(2020, 9, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(5991), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1354), "Another pub" },
                    { new Guid("520adb89-8c38-4e7c-84e2-ed040f139d0d"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1358), new DateTime(2020, 10, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(6002), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1361), "Yet another pub" },
                    { new Guid("9cc05fe6-3d50-4893-a3fe-2e6a65627335"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1374), new DateTime(2020, 11, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(6006), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1377), "Just another pub" },
                    { new Guid("bd5e124a-f52a-437b-86c9-293f9ca7f744"), "music", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1382), new DateTime(2020, 12, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(6011), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1385), "Roundhouse Camden" },
                    { new Guid("bfd99b3c-4284-435c-aa60-f243bb631138"), "travel", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1390), new DateTime(2021, 1, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(6016), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1393), "Somewhere on the Thames" },
                    { new Guid("4493a995-eb08-45bd-8097-0476cd0b4ff0"), "film", "London", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1397), new DateTime(2021, 2, 9, 1, 59, 54, 736, DateTimeKind.Local).AddTicks(6022), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 737, DateTimeKind.Local).AddTicks(1400), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f4d9159b-b58c-4fb2-81b7-2d3fe405d806"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1269), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1366) },
                    { new Guid("1bf940fd-f42b-41d0-8a0c-162fa24f697a"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1422), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1427) },
                    { new Guid("bc31b845-485b-4d4c-b4ea-891189d24a13"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1433), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 738, DateTimeKind.Local).AddTicks(1436) }
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
                values: new object[] { new Guid("6acf2fb8-fc3f-4cd1-9c3d-d792fda6d776"), new Guid("f4d9159b-b58c-4fb2-81b7-2d3fe405d806"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1220), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1296), "JohnDoe@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("68d1edd5-66d2-4d63-907b-ce4bf788e971"), new Guid("1bf940fd-f42b-41d0-8a0c-162fa24f697a"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1532), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1539), "Jane.Smith@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("65ea30c7-73d6-4d5d-8caa-975341de67de"), new Guid("bc31b845-485b-4d4c-b4ea-891189d24a13"), "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1547), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 9, 1, 59, 54, 739, DateTimeKind.Local).AddTicks(1551), "Bruce.Lee@domain.com" });

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
