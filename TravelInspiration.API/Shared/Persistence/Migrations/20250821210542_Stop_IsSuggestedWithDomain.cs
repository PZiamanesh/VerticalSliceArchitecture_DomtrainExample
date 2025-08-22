using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInspiration.API.Shared.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Stop_IsSuggestedWithDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSuggested",
                table: "Stops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5567));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5572));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5777));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5796));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5803));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5809));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 21, 21, 5, 41, 913, DateTimeKind.Utc).AddTicks(5815));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuggested",
                table: "Stops");

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4538));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4554));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 18, 12, 42, 41, 375, DateTimeKind.Utc).AddTicks(4572));
        }
    }
}
