using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProj.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFK2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_VehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "VehicleMakeId",
                table: "VehicleModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehicleMakeId",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleMakeId",
                table: "VehicleModels",
                column: "VehicleMakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels",
                column: "VehicleMakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
