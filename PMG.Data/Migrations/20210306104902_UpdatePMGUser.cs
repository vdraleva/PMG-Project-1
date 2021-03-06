using Microsoft.EntityFrameworkCore.Migrations;

namespace PMG.Data.Migrations
{
    public partial class UpdatePMGUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UCN",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UCN",
                table: "AspNetUsers");
        }
    }
}
