using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Manero_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class testing1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorsId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductsArticleNumber",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductsArticleNumber",
                table: "ProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Sizes_SizesId",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "ProductSizes",
                newName: "ProductEntitySizesEntity");

            migrationBuilder.RenameTable(
                name: "ProductColors",
                newName: "ColorsEntityProductEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizes_SizesId",
                table: "ProductEntitySizesEntity",
                newName: "IX_ProductEntitySizesEntity_SizesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ProductsArticleNumber",
                table: "ColorsEntityProductEntity",
                newName: "IX_ColorsEntityProductEntity_ProductsArticleNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntitySizesEntity",
                table: "ProductEntitySizesEntity",
                columns: new[] { "ProductsArticleNumber", "SizesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColorsEntityProductEntity",
                table: "ColorsEntityProductEntity",
                columns: new[] { "ColorsId", "ProductsArticleNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsEntityProductEntity_Colors_ColorsId",
                table: "ColorsEntityProductEntity",
                column: "ColorsId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsEntityProductEntity_Products_ProductsArticleNumber",
                table: "ColorsEntityProductEntity",
                column: "ProductsArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntitySizesEntity_Products_ProductsArticleNumber",
                table: "ProductEntitySizesEntity",
                column: "ProductsArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntitySizesEntity_Sizes_SizesId",
                table: "ProductEntitySizesEntity",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorsEntityProductEntity_Colors_ColorsId",
                table: "ColorsEntityProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ColorsEntityProductEntity_Products_ProductsArticleNumber",
                table: "ColorsEntityProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntitySizesEntity_Products_ProductsArticleNumber",
                table: "ProductEntitySizesEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntitySizesEntity_Sizes_SizesId",
                table: "ProductEntitySizesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntitySizesEntity",
                table: "ProductEntitySizesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColorsEntityProductEntity",
                table: "ColorsEntityProductEntity");

            migrationBuilder.RenameTable(
                name: "ProductEntitySizesEntity",
                newName: "ProductSizes");

            migrationBuilder.RenameTable(
                name: "ColorsEntityProductEntity",
                newName: "ProductColors");

            migrationBuilder.RenameIndex(
                name: "IX_ProductEntitySizesEntity_SizesId",
                table: "ProductSizes",
                newName: "IX_ProductSizes_SizesId");

            migrationBuilder.RenameIndex(
                name: "IX_ColorsEntityProductEntity_ProductsArticleNumber",
                table: "ProductColors",
                newName: "IX_ProductColors_ProductsArticleNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizes",
                table: "ProductSizes",
                columns: new[] { "ProductsArticleNumber", "SizesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ColorsId", "ProductsArticleNumber" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorName" },
                values: new object[,]
                {
                    { 1, "Blue" },
                    { 2, "Red" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "SizeName" },
                values: new object[,]
                {
                    { 1, "XL" },
                    { 2, "L" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_ColorsId",
                table: "ProductColors",
                column: "ColorsId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_ProductsArticleNumber",
                table: "ProductColors",
                column: "ProductsArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductsArticleNumber",
                table: "ProductSizes",
                column: "ProductsArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Sizes_SizesId",
                table: "ProductSizes",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
