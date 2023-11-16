using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Manero_WebApp.Tests.EnhetsTest;

public class ProductService_Tests
{
    [Fact]
    public async Task GetProductPropertiesAsync_ReturnsCorrectSelectList()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "tempDb")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        using (var context = new DataContext(options))
        {
            var repo = new ProductDbRepo(context);
            var getAllProdcutsService = new GetAllProductsService(context);
            var productService = new ProductService(context, repo, getAllProdcutsService);

            var entities = new List<TagsEntity>
            {
                new TagsEntity { Id = 1, TagName = "sale" },
                new TagsEntity { Id = 2, TagName = "new" },
                new TagsEntity { Id = 4, TagName = "featured" },
                new TagsEntity { Id = 5, TagName = "old" },
            };

            context.Tags.AddRange(entities);
            await context.SaveChangesAsync();

            // Act
            var result = await productService.GetProductPropertiesAsync(context.Tags, tag => tag.TagName, tag => tag.Id.ToString());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entities.Count, result.Count());
            Assert.IsAssignableFrom<IEnumerable<SelectListItem>>(result);
        }
    }

    [Fact]
    public void ImplicitOperator_ConvertsToProductModel()
    {
        //arrange
        var category1 = new CategoriesEntity { Id = 1, CategoryName = "men" };
        var category2 = new CategoriesEntity { Id = 2, CategoryName = "woman" };

        var productEntity = new ProductEntity
        {
            Name = "Test Product",
            Price = 55,
            Description = "description",
            AmountInStock = 10,
            Categories = new List<CategoriesEntity> { category1, category2 }
        };

        //act
        ProductModel productModel = productEntity;

        //assert
        Assert.NotNull(productModel);
        Assert.Equal(productEntity.ArticleNumber, productModel.ArticleNumber);
        Assert.Equal(productEntity.Name, productModel.Name);
        Assert.Equal(productEntity.Price, productModel.Price);
        Assert.Equal(productEntity.Description, productModel.Description);
        Assert.Equal(productEntity.AmountInStock, productModel.AmountInStock);

        //testing categories as an exapmle
        Assert.Equal(productEntity.Categories.Select(c => c.CategoryName), productModel.Categories);
        Assert.Equal(
            productEntity.Reviews.Select(r => new ReviewModel
            {
                Id = r.Id,
                User = r.User,
                ProductId = r.ProductId,
                DateCreated = r.DateCreated,
                Rating = r.Rating,
                ReviewDescription = r.Review
            }),
            productModel.Reviews
        );
    }
}
 