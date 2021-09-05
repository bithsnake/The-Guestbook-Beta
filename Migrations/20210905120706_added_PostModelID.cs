using Microsoft.EntityFrameworkCore.Migrations;

namespace Kursmoment3.Migrations
{
    public partial class added_PostModelID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TOPIC_ID",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TOPIC_ID",
                table: "Post");
        }
    }
}
