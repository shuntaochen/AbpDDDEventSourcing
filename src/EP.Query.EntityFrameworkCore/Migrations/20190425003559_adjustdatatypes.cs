using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Query.Migrations
{
    public partial class adjustdatatypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sourcecontent",
                table: "datasouces",
                newName: "source_content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "source_content",
                table: "datasouces",
                newName: "sourcecontent");
        }
    }
}
