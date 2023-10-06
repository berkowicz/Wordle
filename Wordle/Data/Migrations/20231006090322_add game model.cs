using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Data.Migrations
{
    public partial class addgamemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attempt1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attempt2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attempt3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attempt4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attempt5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameCompleted = table.Column<bool>(type: "bit", nullable: false),
                    UserRefId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_AspNetUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserRefId",
                table: "Games",
                column: "UserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

        }
    }
}
