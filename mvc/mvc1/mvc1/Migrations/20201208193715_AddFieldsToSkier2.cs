using Microsoft.EntityFrameworkCore.Migrations;

namespace mvc1.Migrations
{
    public partial class AddFieldsToSkier2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJ",
                table: "Skier",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsJ",
                table: "Skier");
        }
    }
}
