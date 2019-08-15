namespace Carpet.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddToVehicleMakeAndModelProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Vehicle",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Vehicle",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Vehicle",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Make",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Vehicle",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
