using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Manero_WebApp.Tests.IntegrationsTest
{
    public class AddProductService_Tests
    {
        [Fact]
        public async Task Check_If_Product_Gets_Added_To_Db()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "in_memory_db")
                .Options;

            DataContext db = new(options);
            var productDbRepo = new ProductDbRepo(db);
            var service = new AddProductService(db, productDbRepo);

            var model = new ProductModel
            {
                Name = "Test Product",
                Price = 100,
                Description = "Test Description",
                AmountInStock = 100,
            };

            // Act
            var product = await service.AddAsync(model);
            var result = await db.Products.FirstOrDefaultAsync(x => x.ArticleNumber == product.ArticleNumber);

            // Assert
            Assert.NotNull(result);

        }
    }
}
