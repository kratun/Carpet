namespace Carpet.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveCarrierFromVehicleEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEmployee_Employees_CarrierId",
                table: "VehicleEmployee");

            migrationBuilder.DropIndex(
                name: "IX_VehicleEmployee_CarrierId",
                table: "VehicleEmployee");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "VehicleEmployee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarrierId",
                table: "VehicleEmployee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEmployee_CarrierId",
                table: "VehicleEmployee",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEmployee_Employees_CarrierId",
                table: "VehicleEmployee",
                column: "CarrierId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
