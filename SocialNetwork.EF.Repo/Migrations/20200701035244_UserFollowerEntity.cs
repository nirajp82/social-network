using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class UserFollowerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("fcda0ee6-0b33-4402-a42e-ebdeaba3c027"), "drinks", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(5681), new DateTime(2020, 4, 30, 23, 52, 42, 797, DateTimeKind.Local).AddTicks(897), "Activity 2 months ago", "Past Activity 1", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(6385), "Pub" },
                    { new Guid("43a35040-876a-4694-b49a-c0430749d1fd"), "culture", "Paris", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7143), new DateTime(2020, 5, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1056), "Activity 1 month ago", "Past Activity 2", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7268), "Louvre" },
                    { new Guid("383b73d9-581d-4825-bcff-5960142c1528"), "culture", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7489), new DateTime(2020, 7, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1208), "Activity 1 month in future", "Future Activity 1", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7509), "Natural History Museum" },
                    { new Guid("e61a5a31-c4b6-4d9b-a479-4522a117708d"), "music", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7515), new DateTime(2020, 8, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1220), "Activity 2 months in future", "Future Activity 2", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7519), "O2 Arena" },
                    { new Guid("58768f77-8fcd-42f6-b9d8-de4f1d4cf8df"), "drinks", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7525), new DateTime(2020, 9, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1225), "Activity 3 months in future", "Future Activity 3", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7530), "Another pub" },
                    { new Guid("ae685e8e-2521-4469-9ba2-6ecb935f6ec6"), "drinks", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7535), new DateTime(2020, 10, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1240), "Activity 4 months in future", "Future Activity 4", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7540), "Yet another pub" },
                    { new Guid("b81b1e3d-7c2b-4b41-95de-1ac2129b4bd4"), "drinks", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7559), new DateTime(2020, 11, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1245), "Activity 5 months in future", "Future Activity 5", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7563), "Just another pub" },
                    { new Guid("5753186c-dccc-4196-b75f-cfdab0aa6cd6"), "music", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7569), new DateTime(2020, 12, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1251), "Activity 6 months in future", "Future Activity 6", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7573), "Roundhouse Camden" },
                    { new Guid("6f81c396-3161-4a9d-acf1-648e71a6a98b"), "travel", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7578), new DateTime(2021, 1, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1257), "Activity 2 months ago", "Future Activity 7", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7583), "Somewhere on the Thames" },
                    { new Guid("5124498c-1451-450d-a4c1-9c80b9f43b4c"), "film", "London", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7588), new DateTime(2021, 2, 28, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(1265), "Activity 8 months in future", "Future Activity 8", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 802, DateTimeKind.Local).AddTicks(7592), "Cinema" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Bio", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3c5e848f-a6e1-4b5d-9d98-ca792d0bb1bf"), null, "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3718), "JohnDoe@domain.com", "John", "Doe", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3833) },
                    { new Guid("430ca1f5-aed7-46a9-923b-39da0ec5b83b"), null, "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3907), "Jane.Smith@domain.com", "Jane", "Smith", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3914) },
                    { new Guid("c991a793-4298-4a6a-b45a-8a35955a3af2"), null, "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3922), "Bruce.Lee@domain.com", "Bruce", "Lee", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3928) },
                    { new Guid("743fbd05-33eb-431a-9889-6cc1350a3dd7"), null, "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3937), "NP@domain.com", "Nij", "Patel", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 804, DateTimeKind.Local).AddTicks(3943) }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AppUserId", "CreatedBy", "CreatedDate", "Passoword", "Salt", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("084f7f36-fa5e-4bf5-a693-ebc375fde0cb"), new Guid("3c5e848f-a6e1-4b5d-9d98-ca792d0bb1bf"), "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(8869), "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=", "St0OnTE2Ju3Li9uSnlz/Mg==", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9035), "JohnDoe@domain.com" },
                    { new Guid("e987411c-5bf5-4103-8900-1fef7fbb080a"), new Guid("430ca1f5-aed7-46a9-923b-39da0ec5b83b"), "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9484), "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=", "f9/SzZwluz+xI51/VQQIzg==", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9493), "Jane.Smith@domain.com" },
                    { new Guid("267e9e33-f597-4933-af82-1159ef48b4d5"), new Guid("c991a793-4298-4a6a-b45a-8a35955a3af2"), "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9503), "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=", "DEX8D+3HR9flD6NpGibucQ==", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9510), "Bruce.Lee@domain.com" },
                    { new Guid("29fe9311-4c54-456c-bdad-cbea92b63908"), new Guid("743fbd05-33eb-431a-9889-6cc1350a3dd7"), "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9522), "k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=", "tycaGrI7zbrlLUa1rlq/Eg==", "Seed", new DateTime(2020, 6, 30, 23, 52, 42, 805, DateTimeKind.Local).AddTicks(9528), "string" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollower");

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("383b73d9-581d-4825-bcff-5960142c1528"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("43a35040-876a-4694-b49a-c0430749d1fd"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("5124498c-1451-450d-a4c1-9c80b9f43b4c"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("5753186c-dccc-4196-b75f-cfdab0aa6cd6"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("58768f77-8fcd-42f6-b9d8-de4f1d4cf8df"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("6f81c396-3161-4a9d-acf1-648e71a6a98b"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("ae685e8e-2521-4469-9ba2-6ecb935f6ec6"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("b81b1e3d-7c2b-4b41-95de-1ac2129b4bd4"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("e61a5a31-c4b6-4d9b-a479-4522a117708d"));

            migrationBuilder.DeleteData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: new Guid("fcda0ee6-0b33-4402-a42e-ebdeaba3c027"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("084f7f36-fa5e-4bf5-a693-ebc375fde0cb"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("267e9e33-f597-4933-af82-1159ef48b4d5"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("29fe9311-4c54-456c-bdad-cbea92b63908"));

            migrationBuilder.DeleteData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: new Guid("e987411c-5bf5-4103-8900-1fef7fbb080a"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("3c5e848f-a6e1-4b5d-9d98-ca792d0bb1bf"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("430ca1f5-aed7-46a9-923b-39da0ec5b83b"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("743fbd05-33eb-431a-9889-6cc1350a3dd7"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("c991a793-4298-4a6a-b45a-8a35955a3af2"));

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
        }
    }
}
