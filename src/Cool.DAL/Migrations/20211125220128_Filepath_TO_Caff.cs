using Microsoft.EntityFrameworkCore.Migrations;

namespace Cool.Dal.Migrations
{
    public partial class Filepath_TO_Caff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Caff",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Caff");
        }
    }
}
