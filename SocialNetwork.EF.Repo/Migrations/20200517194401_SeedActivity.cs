using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.EF.Repo.Migrations
{
    public partial class SeedActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Category", "City", "Date", "Description", "Title", "Venue" },
                values: new object[,]
                {
                    { new Guid("aea39df0-7f0f-4665-85c9-0a8d53b4b0c1"), "drinks", "London", new DateTime(2020, 3, 17, 15, 44, 0, 717, DateTimeKind.Local).AddTicks(9773), "Activity 2 months ago", "Past Activity 1", "Pub" },
                    { new Guid("fd94389a-2c2b-4ad9-ab6d-0c103e9580fd"), "culture", "Paris", new DateTime(2020, 4, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(5733), "Activity 1 month ago", "Past Activity 2", "Louvre" },
                    { new Guid("50ac4bc8-34cb-4b33-be47-20f63025d811"), "culture", "London", new DateTime(2020, 6, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(5958), "Activity 1 month in future", "Future Activity 1", "Natural History Museum" },
                    { new Guid("6aefdaf6-cc65-4f73-98f6-f8a640c4fb8e"), "music", "London", new DateTime(2020, 7, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(5974), "Activity 2 months in future", "Future Activity 2", "O2 Arena" },
                    { new Guid("f46d56a0-bbd0-491f-9e93-26efb21e91d5"), "drinks", "London", new DateTime(2020, 8, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(5986), "Activity 3 months in future", "Future Activity 3", "Another pub" },
                    { new Guid("57155bf9-17eb-47de-ab7e-53dbd7f4104c"), "drinks", "London", new DateTime(2020, 9, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(6002), "Activity 4 months in future", "Future Activity 4", "Yet another pub" },
                    { new Guid("507c5cfb-ed68-4a5e-a5c5-f32581d0f840"), "drinks", "London", new DateTime(2020, 10, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(6011), "Activity 5 months in future", "Future Activity 5", "Just another pub" },
                    { new Guid("ddb0593f-2da0-4d15-8f46-51ab3e181cfe"), "music", "London", new DateTime(2020, 11, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(6023), "Activity 6 months in future", "Future Activity 6", "Roundhouse Camden" },
                    { new Guid("ecc74cf8-257a-458d-b1c3-70b68e00f94d"), "travel", "London", new DateTime(2020, 12, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(6032), "Activity 2 months ago", "Future Activity 7", "Somewhere on the Thames" },
                    { new Guid("75f15285-0bae-4e57-b62b-01202e87269c"), "film", "London", new DateTime(2021, 1, 17, 15, 44, 0, 722, DateTimeKind.Local).AddTicks(6046), "Activity 8 months in future", "Future Activity 8", "Cinema" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("507c5cfb-ed68-4a5e-a5c5-f32581d0f840"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("50ac4bc8-34cb-4b33-be47-20f63025d811"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("57155bf9-17eb-47de-ab7e-53dbd7f4104c"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("6aefdaf6-cc65-4f73-98f6-f8a640c4fb8e"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("75f15285-0bae-4e57-b62b-01202e87269c"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("aea39df0-7f0f-4665-85c9-0a8d53b4b0c1"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("ddb0593f-2da0-4d15-8f46-51ab3e181cfe"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("ecc74cf8-257a-458d-b1c3-70b68e00f94d"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("f46d56a0-bbd0-491f-9e93-26efb21e91d5"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("fd94389a-2c2b-4ad9-ab6d-0c103e9580fd"));
        }
    }
}
