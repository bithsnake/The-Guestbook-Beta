using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGuestBook.Migrations
{
    public partial class AddedDescriptionToTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForumTopicDescription",
                table: "Topic",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForumTopicDescription",
                table: "Topic");
        }
    }
}
