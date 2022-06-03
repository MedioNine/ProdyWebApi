using Microsoft.EntityFrameworkCore.Migrations;

namespace Prody.DAL.Migrations
{
    public partial class TestMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kek",
                table: "Musics",
                newName: "Kok");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kok",
                table: "Musics",
                newName: "Kek");
        }
    }
}
