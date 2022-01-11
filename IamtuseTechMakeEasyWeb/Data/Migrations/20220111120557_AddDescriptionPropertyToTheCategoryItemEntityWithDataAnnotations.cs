using Microsoft.EntityFrameworkCore.Migrations;

namespace IamtuseTechMakeEasyWeb.Data.Migrations
{
    public partial class AddDescriptionPropertyToTheCategoryItemEntityWithDataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CategoryItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CategoryItems");
        }
    }
}
