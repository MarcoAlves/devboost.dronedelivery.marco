using Microsoft.EntityFrameworkCore.Migrations;

namespace grupo4.devboost.dronedelivery.Migrations
{
    public partial class situacao_drone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Drone",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Drone");
        }
    }
}
