using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Manero_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class testingtestingtesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ArticleNumber", "AmountInStock", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Hej", "Socka", 15m },
                    { 2, 0, "Hej", "Tröja", 150m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ArticleNumber",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ArticleNumber",
                keyValue: 2);
        }
    }
}
