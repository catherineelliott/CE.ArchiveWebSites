using Microsoft.EntityFrameworkCore.Migrations;

namespace CE.ArchiveWebSites.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaRecordComments",
                columns: table => new
                {
                    MediaRecordCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    MediaRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaRecordComments", x => x.MediaRecordCommentId);
                });

            migrationBuilder.CreateTable(
                name: "WebCards",
                columns: table => new
                {
                    WebCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebCards", x => x.WebCardId);
                });

            migrationBuilder.InsertData(
                table: "MediaRecordComments",
                columns: new[] { "MediaRecordCommentId", "Comment", "CreatedBy", "MediaRecordId" },
                values: new object[,]
                {
                    { 1, "That's lovely", "Catherine", 1 },
                    { 2, "Not great", "Catherine", 2 },
                    { 3, "That's a lovely Playbill", "Catherine", 3 },
                    { 4, "Not a great Playbill", "Catherine", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaRecordComments");

            migrationBuilder.DropTable(
                name: "WebCards");
        }
    }
}
