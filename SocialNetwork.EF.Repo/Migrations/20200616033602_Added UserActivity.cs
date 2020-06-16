using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class AddedUserActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("30d3daf6-e45f-406b-8a7a-c9c9832bfb7e"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("3c793a28-bb40-4c80-a5f3-ccfbfd9dda1e"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("43ba9bf0-5da1-40fe-ab9a-db9d2d7d7092"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("48fda600-d109-4d40-b4f5-c0ea55c104c2"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("54295d0c-b5e4-4868-96e9-5dce2c6dd9e8"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("7c237c6d-8ea4-4ed2-91d5-16f766be6328"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("8179c099-9014-412b-87ed-bd87d41527bf"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("aeb22f5e-85fc-4eba-95bb-5646c063edc9"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("d7202aa0-307f-4a97-a66a-85c4f519f722"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("f26787cc-3895-4485-8375-2fe35d70f1fc"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("411c9873-261f-4424-a228-06039a86f09e"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("5ca511e5-42ec-43fa-ab2e-cf82f272ac8f"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("a9f17d50-3121-4b93-be15-06a9db1ec5e9"));

            migrationBuilder.DeleteData(
                table: "Value",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Value",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Value",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("58829eb4-94da-4903-9b52-8a3150472d9b"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("7a9cbf8c-a162-46c4-b6c6-284c014f1f6d"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("f4771684-36a6-4b2f-bc5b-fd8e8f47b990"));

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

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_AppUserId",
                table: "UserActivity",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivity");

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "Category", "City", "CreatedBy", "CreatedDate", "Date", "Description", "Title", "UpdatedBy", "UpdatedDate", "Venue" },
                values: new object[,]
                {
                    { new Guid("30d3daf6-e45f-406b-8a7a-c9c9832bfb7e"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(1566), new DateTime(2020, 4, 9, 15, 4, 19, 950, DateTimeKind.Local).AddTicks(6896), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(2446), "Pub" },
                    { new Guid("f26787cc-3895-4485-8375-2fe35d70f1fc"), "culture", "Paris", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3367), new DateTime(2020, 5, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6496), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3430), "Louvre" },
                    { new Guid("54295d0c-b5e4-4868-96e9-5dce2c6dd9e8"), "culture", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3458), new DateTime(2020, 7, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6657), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3463), "Natural History Museum" },
                    { new Guid("8179c099-9014-412b-87ed-bd87d41527bf"), "music", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3470), new DateTime(2020, 8, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6671), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3475), "O2 Arena" },
                    { new Guid("aeb22f5e-85fc-4eba-95bb-5646c063edc9"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3481), new DateTime(2020, 9, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6678), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3486), "Another pub" },
                    { new Guid("d7202aa0-307f-4a97-a66a-85c4f519f722"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3511), new DateTime(2020, 10, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6693), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3516), "Yet another pub" },
                    { new Guid("43ba9bf0-5da1-40fe-ab9a-db9d2d7d7092"), "drinks", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3523), new DateTime(2020, 11, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6700), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3528), "Just another pub" },
                    { new Guid("48fda600-d109-4d40-b4f5-c0ea55c104c2"), "music", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3536), new DateTime(2020, 12, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6707), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3540), "Roundhouse Camden" },
                    { new Guid("3c793a28-bb40-4c80-a5f3-ccfbfd9dda1e"), "travel", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3546), new DateTime(2021, 1, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6713), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3551), "Somewhere on the Thames" },
                    { new Guid("7c237c6d-8ea4-4ed2-91d5-16f766be6328"), "film", "London", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3557), new DateTime(2021, 2, 9, 15, 4, 19, 955, DateTimeKind.Local).AddTicks(6722), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 956, DateTimeKind.Local).AddTicks(3562), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f4771684-36a6-4b2f-bc5b-fd8e8f47b990"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8112), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8222) },
                    { new Guid("58829eb4-94da-4903-9b52-8a3150472d9b"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8295), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8302) },
                    { new Guid("7a9cbf8c-a162-46c4-b6c6-284c014f1f6d"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8310), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 957, DateTimeKind.Local).AddTicks(8315) }
                });

            migrationBuilder.InsertData(
                table: "Value",
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
                values: new object[] { new Guid("a9f17d50-3121-4b93-be15-06a9db1ec5e9"), new Guid("f4771684-36a6-4b2f-bc5b-fd8e8f47b990"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1364), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1436), "JohnDoe@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("5ca511e5-42ec-43fa-ab2e-cf82f272ac8f"), new Guid("58829eb4-94da-4903-9b52-8a3150472d9b"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1801), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1814), "Jane.Smith@domain.com" });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { new Guid("411c9873-261f-4424-a228-06039a86f09e"), new Guid("7a9cbf8c-a162-46c4-b6c6-284c014f1f6d"), "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1825), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 9, 15, 4, 19, 959, DateTimeKind.Local).AddTicks(1830), "Bruce.Lee@domain.com" });
        }
    }
}
