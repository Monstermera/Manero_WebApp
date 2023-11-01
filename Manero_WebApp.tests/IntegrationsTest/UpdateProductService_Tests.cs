using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Manero_WebApp.Tests.IntegrationsTest;

public class UpdateProductService_Tests
{
    [Fact]
    public async Task Check_If_Product_Gets_Updated()
    {
        //Arrange
        var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: "in_memory_db")
        .Options;

        DataContext db = new(options);
        ProductDbRepo repo = new ProductDbRepo(db);
        AddProductService addProduct = new(db, repo);
        UpdateProductService servicetest = new(db);

        ProductModel product = new ProductModel()
        {
            Name = "Product Name",
            Price = 10,
            Description = "Description",
            AmountInStock = 2
        };
        ProductModel updatedProduct = new ProductModel()
        {
            ArticleNumber = product.ArticleNumber,
            Name = "Product Name Updated",
            Price = 15,
            Description = "Description Updated",
            AmountInStock = 5
        };
        var result = await addProduct.AddAsync(product);
        await db.SaveChangesAsync();

        //Act
        var updatedResult = await servicetest.UpdateAsync(updatedProduct);
        var updatedProductResult = await db.Products.FirstOrDefaultAsync(x => x.ArticleNumber == result.ArticleNumber);

        //Assert
        Assert.Null(result);
        Assert.Equal(result, updatedProductResult);

    }
}
