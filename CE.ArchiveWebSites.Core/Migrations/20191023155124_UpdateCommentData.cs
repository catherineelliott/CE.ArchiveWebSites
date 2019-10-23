using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.ArchiveWebSites.Core.Migrations
{
    public partial class UpdateCommentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaResourceComments",
                keyColumn: "MediaResourceCommentId",
                keyValue: 3,
                column: "MediaResourceId",
                value: 9999);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaResourceComments",
                keyColumn: "MediaResourceCommentId",
                keyValue: 3,
                column: "MediaResourceId",
                value: 3);
        }
    }
}
