using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.ArchiveWebSites.Core.Migrations
{
    public partial class UpdateCommentData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaRecordComments",
                keyColumn: "MediaRecordCommentId",
                keyValue: 3,
                column: "MediaRecordId",
                value: 9999);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MediaRecordComments",
                keyColumn: "MediaRecordCommentId",
                keyValue: 3,
                column: "MediaRecordId",
                value: 3);
        }
    }
}
