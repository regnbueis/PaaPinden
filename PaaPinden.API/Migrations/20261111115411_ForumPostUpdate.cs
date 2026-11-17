using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaaPinden.API.Migrations
{
    /// <inheritdoc />
    public partial class ForumPostUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Topic",
                table: "ForumPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "ForumPosts");
        }
    }
}
