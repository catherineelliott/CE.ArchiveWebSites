using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.ArchiveWebSites.Core.Migrations
{
    public partial class AddApprovalStatusToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "MediaRecordComments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "MediaRecordComments");
        }
    }
}
