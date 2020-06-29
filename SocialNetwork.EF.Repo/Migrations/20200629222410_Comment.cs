using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("13ed81c7-6b31-411f-b945-e529d0fc7c69"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("39427a54-8315-447b-9bb2-990db9ce6afa"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("3c738613-6312-4d62-bf14-93def26593e6"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("3f3434ba-268a-4a67-987e-52a76f96c130"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("6fb3789b-5a76-4812-9717-5c28b5b9a33c"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("8f967057-de73-49ac-89e6-8e2b5c64b840"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("d5b6d235-96b8-4020-9b33-5802d245df15"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("eb1f7fd9-a352-4907-862a-8b0f7e975853"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("efed3e18-0910-4bd8-aa70-17fce47c3cba"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("f4676b1a-d1c3-43c3-b737-781370d66144"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("983d5598-0960-43a0-9f7d-a9c3f4e2e751"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("aaf7dec0-ce9b-4e49-afe4-498376e0e307"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("b1115bf2-5639-4f48-a710-f02933c82c4d"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("de0ce645-0350-43a4-9ef3-7676dac4824e"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("551e9172-9202-4985-9865-4df2f53fcf2b"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("78151b38-ad7f-4e27-b30d-3d8690c73237"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("b0c2f35c-77ba-4b69-9c74-97d02f3aa27c"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("b4872b74-272e-42f2-9631-e43baec8a059"));

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

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("2b7fd22d-f0ee-4a50-a6fb-871b8fd06a1d"), "drinks", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(5021), new DateTime(2020, 4, 29, 18, 24, 9, 359, DateTimeKind.Local).AddTicks(34), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(6317), "Pub" },
                    { new Guid("9192118e-bc9a-4775-bf25-922f4e81629c"), "culture", "Paris", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7550), new DateTime(2020, 5, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8609), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7628), "Louvre" },
                    { new Guid("1b25ee77-4ea9-4812-902f-cdfb538c874f"), "culture", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7673), new DateTime(2020, 7, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8821), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7680), "Natural History Museum" },
                    { new Guid("fef6aef4-be94-4b36-b814-5d825bbe6f2a"), "music", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7710), new DateTime(2020, 8, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8840), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7717), "O2 Arena" },
                    { new Guid("569f94f0-3e42-4160-96ab-bd7a79920f1c"), "drinks", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7726), new DateTime(2020, 9, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8850), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7733), "Another pub" },
                    { new Guid("29fcdf4a-6edf-43e8-b23e-234b7ebdf2e1"), "drinks", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7741), new DateTime(2020, 10, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8867), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7747), "Yet another pub" },
                    { new Guid("e9c12889-6947-4224-a8ce-7212f929c7f2"), "drinks", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7755), new DateTime(2020, 11, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8876), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7762), "Just another pub" },
                    { new Guid("224c2d36-98dc-4e69-9e07-1e87f4219e74"), "music", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7771), new DateTime(2020, 12, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8885), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7777), "Roundhouse Camden" },
                    { new Guid("7138ce38-45c6-4412-b816-7879c4c332d0"), "travel", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7785), new DateTime(2021, 1, 29, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8894), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7792), "Somewhere on the Thames" },
                    { new Guid("ce788c7f-df8b-416f-bdf7-ccc0cb66b0ee"), "film", "London", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7801), new DateTime(2021, 2, 28, 18, 24, 9, 364, DateTimeKind.Local).AddTicks(8908), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 365, DateTimeKind.Local).AddTicks(7807), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Bio", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f4dab947-cb80-403c-ac06-0b4d8f43d86f"), null, "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5707), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5831) },
                    { new Guid("bf0f0369-0e79-443c-a4be-2e096cfadc19"), null, "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5934), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5944) },
                    { new Guid("75da9d7c-fea3-4f22-862d-e5a40d004e65"), null, "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5955), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5961) },
                    { new Guid("41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905"), null, "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5971), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 367, DateTimeKind.Local).AddTicks(5977) }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("864c11fe-e06b-4f54-a504-f94376c8f770"), new Guid("f4dab947-cb80-403c-ac06-0b4d8f43d86f"), "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(3893), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(3987), "JohnDoe@domain.com" },
                    { new Guid("98221c13-b39e-4058-a52f-b87c6ff95c98"), new Guid("bf0f0369-0e79-443c-a4be-2e096cfadc19"), "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4383), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4396), "Jane.Smith@domain.com" },
                    { new Guid("c7112578-f044-4f48-9f41-7a8842f0d1ef"), new Guid("75da9d7c-fea3-4f22-862d-e5a40d004e65"), "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4412), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4418), "Bruce.Lee@domain.com" },
                    { new Guid("693017b7-fd2d-431f-a5ca-6bf903101e48"), new Guid("41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905"), "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4430), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 29, 18, 24, 9, 369, DateTimeKind.Local).AddTicks(4437), "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ActivityId",
                table: "Comment",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("1b25ee77-4ea9-4812-902f-cdfb538c874f"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("224c2d36-98dc-4e69-9e07-1e87f4219e74"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("29fcdf4a-6edf-43e8-b23e-234b7ebdf2e1"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("2b7fd22d-f0ee-4a50-a6fb-871b8fd06a1d"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("569f94f0-3e42-4160-96ab-bd7a79920f1c"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("7138ce38-45c6-4412-b816-7879c4c332d0"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("9192118e-bc9a-4775-bf25-922f4e81629c"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("ce788c7f-df8b-416f-bdf7-ccc0cb66b0ee"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("e9c12889-6947-4224-a8ce-7212f929c7f2"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("fef6aef4-be94-4b36-b814-5d825bbe6f2a"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("693017b7-fd2d-431f-a5ca-6bf903101e48"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("864c11fe-e06b-4f54-a504-f94376c8f770"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("98221c13-b39e-4058-a52f-b87c6ff95c98"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("c7112578-f044-4f48-9f41-7a8842f0d1ef"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("75da9d7c-fea3-4f22-862d-e5a40d004e65"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("bf0f0369-0e79-443c-a4be-2e096cfadc19"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("f4dab947-cb80-403c-ac06-0b4d8f43d86f"));

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("eb1f7fd9-a352-4907-862a-8b0f7e975853"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(8543), new DateTime(2020, 4, 21, 19, 7, 41, 609, DateTimeKind.Local).AddTicks(5494), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(9734), "Pub" },
                    { new Guid("efed3e18-0910-4bd8-aa70-17fce47c3cba"), "culture", "Paris", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1417), new DateTime(2020, 5, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(377), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1533), "Louvre" },
                    { new Guid("6fb3789b-5a76-4812-9717-5c28b5b9a33c"), "culture", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1611), new DateTime(2020, 7, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(632), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1624), "Natural History Museum" },
                    { new Guid("d5b6d235-96b8-4020-9b33-5802d245df15"), "music", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1639), new DateTime(2020, 8, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(656), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1651), "O2 Arena" },
                    { new Guid("f4676b1a-d1c3-43c3-b737-781370d66144"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1666), new DateTime(2020, 9, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(670), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1674), "Another pub" },
                    { new Guid("39427a54-8315-447b-9bb2-990db9ce6afa"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1685), new DateTime(2020, 10, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(692), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1692), "Yet another pub" },
                    { new Guid("3c738613-6312-4d62-bf14-93def26593e6"), "drinks", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1701), new DateTime(2020, 11, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(704), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1708), "Just another pub" },
                    { new Guid("3f3434ba-268a-4a67-987e-52a76f96c130"), "music", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1729), new DateTime(2020, 12, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(717), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1736), "Roundhouse Camden" },
                    { new Guid("13ed81c7-6b31-411f-b945-e529d0fc7c69"), "travel", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1745), new DateTime(2021, 1, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(730), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1752), "Somewhere on the Thames" },
                    { new Guid("8f967057-de73-49ac-89e6-8e2b5c64b840"), "film", "London", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1761), new DateTime(2021, 2, 21, 19, 7, 41, 616, DateTimeKind.Local).AddTicks(747), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 617, DateTimeKind.Local).AddTicks(1768), "Cinema" }
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
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("aaf7dec0-ce9b-4e49-afe4-498376e0e307"), new Guid("b0c2f35c-77ba-4b69-9c74-97d02f3aa27c"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(586), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(873), "JohnDoe@domain.com" },
                    { new Guid("b1115bf2-5639-4f48-a710-f02933c82c4d"), new Guid("78151b38-ad7f-4e27-b30d-3d8690c73237"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1585), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1598), "Jane.Smith@domain.com" },
                    { new Guid("de0ce645-0350-43a4-9ef3-7676dac4824e"), new Guid("551e9172-9202-4985-9865-4df2f53fcf2b"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1613), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1620), "Bruce.Lee@domain.com" },
                    { new Guid("983d5598-0960-43a0-9f7d-a9c3f4e2e751"), new Guid("b4872b74-272e-42f2-9631-e43baec8a059"), "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1631), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 21, 19, 7, 41, 622, DateTimeKind.Local).AddTicks(1638), "string" }
                });
        }
    }
}
