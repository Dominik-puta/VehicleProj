using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProj.Migrations
{
    /// <inheritdoc />
    public partial class AddedMakeNametoVehicleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MakeName",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MakeName",
                table: "VehicleModels");
        }
    }
}
