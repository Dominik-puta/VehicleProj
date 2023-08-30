using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProj.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedAtatbothmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VehicleModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VehicleMakes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VehicleMakes");
        }
    }
}
