using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertiesWebService.DatabaseMigrator.Migrations
{
    /// <inheritdoc />
    public partial class DbInitWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictPropertyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictPropertyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictSpaceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictSpaceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(8192)", maxLength: 8192, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DictPropertyType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Space",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Size = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(8192)", maxLength: 8192, nullable: true),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Space", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Space_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Space_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DictSpaceType",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DictPropertyType",
                columns: new[] { "Id", "CreatedDate", "IsActive", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(115), true, null, "Apartment" },
                    { 2, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(120), true, null, "House" },
                    { 3, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(121), true, null, "Condo" },
                    { 4, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(122), true, null, "Villa" },
                    { 5, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(124), true, null, "Cottage" }
                });

            migrationBuilder.InsertData(
                table: "DictSpaceType",
                columns: new[] { "Id", "CreatedDate", "IsActive", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(439), true, null, "Living Room" },
                    { 2, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(441), true, null, "Bedroom" },
                    { 3, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(442), true, null, "Kitchen" },
                    { 4, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(443), true, null, "Office room" },
                    { 5, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(445), true, null, "Garage" },
                    { 6, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(446), true, null, "Garden" },
                    { 7, new DateTime(2025, 8, 27, 11, 42, 50, 26, DateTimeKind.Utc).AddTicks(447), true, null, "Balcony" }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "Description", "Price", "TypeId" },
                values: new object[,]
                {
                    { 1, "1 Ocean View Drive", "Modern apartment with stunning ocean views.", 1200.0m, 1 },
                    { 2, "22 Maple Street", "Cozy house in a quiet suburban neighborhood.", 950.0m, 2 },
                    { 3, "5 City Center Plaza", "Luxury condo in the heart of downtown.", 1800.0m, 3 },
                    { 4, "77 Mountain Retreat", "Spacious villa nestled in the mountains.", 2500.0m, 4 },
                    { 5, "9 Lakeside Lane", "Charming cottage by the lake.", 1100.0m, 5 },
                    { 6, "101 Riverside Avenue", "Apartment with river views and modern amenities.", 1300.0m, 1 },
                    { 7, "33 Forest Path", "House surrounded by beautiful forest scenery.", 1050.0m, 2 },
                    { 8, "8 Grand Boulevard", "Condo with access to city attractions.", 2000.0m, 3 },
                    { 9, "12 Sunny Meadows", "Villa with large garden and sunny rooms.", 2300.0m, 4 },
                    { 10, "44 Hilltop Crescent", "Cottage with panoramic hilltop views.", 1150.0m, 5 },
                    { 11, "2 Seaside Promenade", "Apartment steps from the beach.", 1400.0m, 1 },
                    { 12, "18 Country Lane", "House with spacious backyard in the countryside.", 980.0m, 2 },
                    { 13, "6 Urban Heights", "Condo with skyline views.", 1750.0m, 3 },
                    { 14, "55 Vineyard Road", "Villa surrounded by vineyards.", 2600.0m, 4 },
                    { 15, "3 Harbor Point", "Cottage with harbor access.", 1200.0m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Space",
                columns: new[] { "Id", "Description", "PropertyId", "Size", "TypeId" },
                values: new object[,]
                {
                    { 1, "Spacious living room with natural light. (Property 1)", 1, 21.0m, 1 },
                    { 2, "Cozy master bedroom with ensuite bathroom. (Property 1)", 1, 31.0m, 2 },
                    { 3, "Modern kitchen with high-end appliances. (Property 1)", 1, 41.0m, 3 },
                    { 4, "Home office with inspiring views. (Property 1)", 1, 51.0m, 4 },
                    { 5, "Secure garage with extra storage. (Property 1)", 1, 61.0m, 5 },
                    { 6, "Beautiful garden with seasonal flowers. (Property 1)", 1, 71.0m, 6 },
                    { 7, "Balcony perfect for morning coffee. (Property 1)", 1, 81.0m, 7 },
                    { 8, "Spacious living room with natural light. (Property 2)", 2, 22.0m, 1 },
                    { 9, "Cozy master bedroom with ensuite bathroom. (Property 2)", 2, 32.0m, 2 },
                    { 10, "Modern kitchen with high-end appliances. (Property 2)", 2, 42.0m, 3 },
                    { 11, "Home office with inspiring views. (Property 2)", 2, 52.0m, 4 },
                    { 12, "Secure garage with extra storage. (Property 2)", 2, 62.0m, 5 },
                    { 13, "Spacious living room with natural light. (Property 3)", 3, 23.0m, 1 },
                    { 14, "Cozy master bedroom with ensuite bathroom. (Property 3)", 3, 33.0m, 2 },
                    { 15, "Modern kitchen with high-end appliances. (Property 3)", 3, 43.0m, 3 },
                    { 16, "Home office with inspiring views. (Property 3)", 3, 53.0m, 4 },
                    { 17, "Secure garage with extra storage. (Property 3)", 3, 63.0m, 5 },
                    { 18, "Beautiful garden with seasonal flowers. (Property 3)", 3, 73.0m, 6 },
                    { 19, "Spacious living room with natural light. (Property 4)", 4, 24.0m, 1 },
                    { 20, "Cozy master bedroom with ensuite bathroom. (Property 4)", 4, 34.0m, 2 },
                    { 21, "Modern kitchen with high-end appliances. (Property 4)", 4, 44.0m, 3 },
                    { 22, "Home office with inspiring views. (Property 4)", 4, 54.0m, 4 },
                    { 23, "Secure garage with extra storage. (Property 4)", 4, 64.0m, 5 },
                    { 24, "Beautiful garden with seasonal flowers. (Property 4)", 4, 74.0m, 6 },
                    { 25, "Balcony perfect for morning coffee. (Property 4)", 4, 84.0m, 7 },
                    { 26, "Spacious living room with natural light. (Property 4)", 4, 94.0m, 1 },
                    { 27, "Spacious living room with natural light. (Property 5)", 5, 25.0m, 1 },
                    { 28, "Cozy master bedroom with ensuite bathroom. (Property 5)", 5, 35.0m, 2 },
                    { 29, "Modern kitchen with high-end appliances. (Property 5)", 5, 45.0m, 3 },
                    { 30, "Home office with inspiring views. (Property 5)", 5, 55.0m, 4 },
                    { 31, "Secure garage with extra storage. (Property 5)", 5, 65.0m, 5 },
                    { 32, "Beautiful garden with seasonal flowers. (Property 5)", 5, 75.0m, 6 },
                    { 33, "Balcony perfect for morning coffee. (Property 5)", 5, 85.0m, 7 },
                    { 34, "Spacious living room with natural light. (Property 5)", 5, 95.0m, 1 },
                    { 35, "Spacious living room with natural light. (Property 6)", 6, 26.0m, 1 },
                    { 36, "Cozy master bedroom with ensuite bathroom. (Property 6)", 6, 36.0m, 2 },
                    { 37, "Modern kitchen with high-end appliances. (Property 6)", 6, 46.0m, 3 },
                    { 38, "Home office with inspiring views. (Property 6)", 6, 56.0m, 4 },
                    { 39, "Secure garage with extra storage. (Property 6)", 6, 66.0m, 5 },
                    { 40, "Beautiful garden with seasonal flowers. (Property 6)", 6, 76.0m, 6 },
                    { 41, "Balcony perfect for morning coffee. (Property 6)", 6, 86.0m, 7 },
                    { 42, "Spacious living room with natural light. (Property 7)", 7, 27.0m, 1 },
                    { 43, "Cozy master bedroom with ensuite bathroom. (Property 7)", 7, 37.0m, 2 },
                    { 44, "Modern kitchen with high-end appliances. (Property 7)", 7, 47.0m, 3 },
                    { 45, "Home office with inspiring views. (Property 7)", 7, 57.0m, 4 },
                    { 46, "Secure garage with extra storage. (Property 7)", 7, 67.0m, 5 },
                    { 47, "Beautiful garden with seasonal flowers. (Property 7)", 7, 77.0m, 6 },
                    { 48, "Balcony perfect for morning coffee. (Property 7)", 7, 87.0m, 7 },
                    { 49, "Spacious living room with natural light. (Property 8)", 8, 28.0m, 1 },
                    { 50, "Cozy master bedroom with ensuite bathroom. (Property 8)", 8, 38.0m, 2 },
                    { 51, "Modern kitchen with high-end appliances. (Property 8)", 8, 48.0m, 3 },
                    { 52, "Home office with inspiring views. (Property 8)", 8, 58.0m, 4 },
                    { 53, "Secure garage with extra storage. (Property 8)", 8, 68.0m, 5 },
                    { 54, "Beautiful garden with seasonal flowers. (Property 8)", 8, 78.0m, 6 },
                    { 55, "Balcony perfect for morning coffee. (Property 8)", 8, 88.0m, 7 },
                    { 56, "Spacious living room with natural light. (Property 8)", 8, 98.0m, 1 },
                    { 57, "Spacious living room with natural light. (Property 9)", 9, 29.0m, 1 },
                    { 58, "Cozy master bedroom with ensuite bathroom. (Property 9)", 9, 39.0m, 2 },
                    { 59, "Modern kitchen with high-end appliances. (Property 9)", 9, 49.0m, 3 },
                    { 60, "Home office with inspiring views. (Property 9)", 9, 59.0m, 4 },
                    { 61, "Secure garage with extra storage. (Property 9)", 9, 69.0m, 5 },
                    { 62, "Beautiful garden with seasonal flowers. (Property 9)", 9, 79.0m, 6 },
                    { 63, "Balcony perfect for morning coffee. (Property 9)", 9, 89.0m, 7 },
                    { 64, "Spacious living room with natural light. (Property 9)", 9, 99.0m, 1 },
                    { 65, "Spacious living room with natural light. (Property 10)", 10, 30.0m, 1 },
                    { 66, "Cozy master bedroom with ensuite bathroom. (Property 10)", 10, 40.0m, 2 },
                    { 67, "Modern kitchen with high-end appliances. (Property 10)", 10, 50.0m, 3 },
                    { 68, "Home office with inspiring views. (Property 10)", 10, 60.0m, 4 },
                    { 69, "Secure garage with extra storage. (Property 10)", 10, 70.0m, 5 },
                    { 70, "Beautiful garden with seasonal flowers. (Property 10)", 10, 80.0m, 6 },
                    { 71, "Balcony perfect for morning coffee. (Property 10)", 10, 90.0m, 7 },
                    { 72, "Spacious living room with natural light. (Property 10)", 10, 100.0m, 1 },
                    { 73, "Spacious living room with natural light. (Property 11)", 11, 31.0m, 1 },
                    { 74, "Cozy master bedroom with ensuite bathroom. (Property 11)", 11, 41.0m, 2 },
                    { 75, "Modern kitchen with high-end appliances. (Property 11)", 11, 51.0m, 3 },
                    { 76, "Home office with inspiring views. (Property 11)", 11, 61.0m, 4 },
                    { 77, "Secure garage with extra storage. (Property 11)", 11, 71.0m, 5 },
                    { 78, "Beautiful garden with seasonal flowers. (Property 11)", 11, 81.0m, 6 },
                    { 79, "Spacious living room with natural light. (Property 12)", 12, 32.0m, 1 },
                    { 80, "Cozy master bedroom with ensuite bathroom. (Property 12)", 12, 42.0m, 2 },
                    { 81, "Modern kitchen with high-end appliances. (Property 12)", 12, 52.0m, 3 },
                    { 82, "Home office with inspiring views. (Property 12)", 12, 62.0m, 4 },
                    { 83, "Secure garage with extra storage. (Property 12)", 12, 72.0m, 5 },
                    { 84, "Spacious living room with natural light. (Property 13)", 13, 33.0m, 1 },
                    { 85, "Cozy master bedroom with ensuite bathroom. (Property 13)", 13, 43.0m, 2 },
                    { 86, "Modern kitchen with high-end appliances. (Property 13)", 13, 53.0m, 3 },
                    { 87, "Home office with inspiring views. (Property 13)", 13, 63.0m, 4 },
                    { 88, "Secure garage with extra storage. (Property 13)", 13, 73.0m, 5 },
                    { 89, "Beautiful garden with seasonal flowers. (Property 13)", 13, 83.0m, 6 },
                    { 90, "Balcony perfect for morning coffee. (Property 13)", 13, 93.0m, 7 },
                    { 91, "Spacious living room with natural light. (Property 13)", 13, 103.0m, 1 },
                    { 92, "Spacious living room with natural light. (Property 14)", 14, 34.0m, 1 },
                    { 93, "Cozy master bedroom with ensuite bathroom. (Property 14)", 14, 44.0m, 2 },
                    { 94, "Modern kitchen with high-end appliances. (Property 14)", 14, 54.0m, 3 },
                    { 95, "Home office with inspiring views. (Property 14)", 14, 64.0m, 4 },
                    { 96, "Secure garage with extra storage. (Property 14)", 14, 74.0m, 5 },
                    { 97, "Beautiful garden with seasonal flowers. (Property 14)", 14, 84.0m, 6 },
                    { 98, "Spacious living room with natural light. (Property 15)", 15, 35.0m, 1 },
                    { 99, "Cozy master bedroom with ensuite bathroom. (Property 15)", 15, 45.0m, 2 },
                    { 100, "Modern kitchen with high-end appliances. (Property 15)", 15, 55.0m, 3 },
                    { 101, "Home office with inspiring views. (Property 15)", 15, 65.0m, 4 },
                    { 102, "Secure garage with extra storage. (Property 15)", 15, 75.0m, 5 },
                    { 103, "Beautiful garden with seasonal flowers. (Property 15)", 15, 85.0m, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictPropertyType_Name",
                table: "DictPropertyType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DictSpaceType_Name",
                table: "DictSpaceType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Address",
                table: "Property",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Property_Address_Price",
                table: "Property",
                columns: new[] { "Address", "Price" });

            migrationBuilder.CreateIndex(
                name: "IX_Property_Price",
                table: "Property",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Property_TypeId",
                table: "Property",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_TypeId_Address",
                table: "Property",
                columns: new[] { "TypeId", "Address" });

            migrationBuilder.CreateIndex(
                name: "IX_Property_TypeId_Price",
                table: "Property",
                columns: new[] { "TypeId", "Price" });

            migrationBuilder.CreateIndex(
                name: "IX_Space_PropertyId",
                table: "Space",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Space_PropertyId_Size",
                table: "Space",
                columns: new[] { "PropertyId", "Size" });

            migrationBuilder.CreateIndex(
                name: "IX_Space_PropertyId_TypeId",
                table: "Space",
                columns: new[] { "PropertyId", "TypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Space_TypeId",
                table: "Space",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Space_TypeId_Size",
                table: "Space",
                columns: new[] { "TypeId", "Size" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Space");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "DictSpaceType");

            migrationBuilder.DropTable(
                name: "DictPropertyType");
        }
    }
}
