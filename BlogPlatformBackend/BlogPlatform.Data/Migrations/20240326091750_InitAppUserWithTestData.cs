using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitAppUserWithTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 1, "123456", "TestUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
