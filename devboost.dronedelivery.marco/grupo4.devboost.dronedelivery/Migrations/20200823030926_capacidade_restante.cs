using Microsoft.EntityFrameworkCore.Migrations;

namespace grupo4.devboost.dronedelivery.Migrations
{
    public partial class capacidade_restante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacidadeRestante",
                table: "Drone",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeRestante",
                table: "Drone");
        }
    }
}
