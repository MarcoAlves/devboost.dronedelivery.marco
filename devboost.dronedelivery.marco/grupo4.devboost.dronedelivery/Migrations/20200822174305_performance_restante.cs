using Microsoft.EntityFrameworkCore.Migrations;

namespace grupo4.devboost.dronedelivery.Migrations
{
    public partial class performance_restante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PerformanceRestante",
                table: "Drone",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformanceRestante",
                table: "Drone");
        }
    }
}
