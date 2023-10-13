using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Data.Migrations
{
    public partial class bump : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Highscores",
                table: "Highscores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Highscores");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Highscores",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "GameRefId",
                table: "Highscores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Highscores",
                table: "Highscores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Highscores_GameRefId",
                table: "Highscores",
                column: "GameRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Highscores_Games_GameRefId",
                table: "Highscores",
                column: "GameRefId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highscores_Games_GameRefId",
                table: "Highscores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Highscores",
                table: "Highscores");

            migrationBuilder.DropIndex(
                name: "IX_Highscores_GameRefId",
                table: "Highscores");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Highscores");

            migrationBuilder.DropColumn(
                name: "GameRefId",
                table: "Highscores");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Highscores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }
    }
}
