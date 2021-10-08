using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class secondmigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Country", "FirstName", "LastName" },
                values: new object[] { 1, "Macedonia", "Name", "Surname" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Country", "FirstName", "LastName" },
                values: new object[] { 2, "USA", "Name2", "Surname2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
