using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Data.Migrations
{
    public partial class added_completeTime_to_GameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompleteTime",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteTime",
                table: "Games");
        }
    }
}
