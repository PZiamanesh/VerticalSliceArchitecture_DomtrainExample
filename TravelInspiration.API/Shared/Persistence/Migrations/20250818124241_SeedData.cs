using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInspiration.API.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Itineraries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "LastModifiedAt", "LastModifiedBy", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4313), "DATASEED", "Five great days in Paris", null, null, "A Trip to Paris", "dummyuserid" },
                    { 2, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4315), "DATASEED", "A week in beautiful Antwerp", null, null, "Antwerp Extravaganza", "dummyuserid" }
                });

            migrationBuilder.InsertData(
                table: "Stops",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ImageUri", "ItineraryId", "LastModifiedAt", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4538), "DATASEED", "https://localhost:7120/images/eiffeltower.jpg", 1, null, null, "The Eiffel Tower" },
                    { 2, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4548), "DATASEED", "https://localhost:7120/images/louvre.jpg", 1, null, null, "The Louvre" },
                    { 3, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4554), "DATASEED", "https://localhost:7120/images/perelachaise.jpg", 1, null, null, "Père Lachaise Cemetery" },
                    { 4, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4561), "DATASEED", "https://localhost:7120/images/royalmuseum.jpg", 2, null, null, "The Royal Museum of Beautiful Arts" },
                    { 5, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4566), "DATASEED", "https://localhost:7120/images/stpauls.jpg", 2, null, null, "Saint Paul's Church" },
                    { 6, new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4572), "DATASEED", "https://localhost:7120/images/michelin.jpg", 2, null, null, "Michelin Restaurant Visit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
