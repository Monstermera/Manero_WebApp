using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.ProductsViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Manero_WebApp.Tests.EnhetsTest;

public class ProductService_Tests
{
    private readonly Mock<IGetAllProductsService> _getAllProductsService;

    public ProductService_Tests()
    {
        _getAllProductsService = new Mock<IGetAllProductsService>();
    }

    [Fact]
    public async Task PopulateCategoryViewModel_ShouldReturnValidModel()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>() 
            .UseInMemoryDatabase(databaseName: "in_memory_db")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        using (var context = new DataContext(options))
        {
            var repo = new ProductDbRepo(context);
            var service = new ProductService(context, repo, _getAllProductsService.Object);

            var fakeProducts = new List<ProductModel>
            {
                new ProductModel()
                {
                    ArticleNumber = Guid.NewGuid(),
                    Name = "Product A",
                    Categories = new List<string> { "woman", "dress" },
                },
                new ProductModel()
                {
                    ArticleNumber = Guid.NewGuid(),
                    Name = "Product B",
                    Categories = new List<string> { "man", "shoes" },
                },

            };

            var fakeCategories = new List<CategoriesEntity> 
            {
                new CategoriesEntity() { Id = 1, CategoryName = "man" },
                new CategoriesEntity() { Id = 2, CategoryName = "woman" },
                new CategoriesEntity() { Id = 3, CategoryName = "dress" }
            };

            // Act
            _getAllProductsService.Setup(s => s.GetAllAsync()).ReturnsAsync(fakeProducts);
            context.Categories.AddRange(fakeCategories);
            await context.SaveChangesAsync();

            var categoryToTest = "something";
            var result = await service.PopulateCategoryViewModel(categoryToTest);

            // Assert
            Assert.NotNull(result.Categories);
            Assert.NotNull(result.Products);

            Assert.IsType<CategoriesViewModel>(result);
            Assert.True(result.Products.All(p => p.Categories.Contains(categoryToTest)));
            Assert.True(result.Products.All(p => p is ProductModel));
        }
    }
}
