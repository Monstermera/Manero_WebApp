using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero_WebApp.Tests.IntegrationsTest
{
	public class DeleteOneProductService_Tests
	{
		[Fact]
		public async Task Check_If_Product_Gets_Deleted_From_Db()
		{
			//Arrange
			var serviceProvider = new ServiceCollection()
			.AddEntityFrameworkInMemoryDatabase()
			.BuildServiceProvider();

			var options = new DbContextOptionsBuilder<DataContext>()
			.UseInMemoryDatabase(databaseName: "in_memory_db")
			.Options;

			DataContext db = new(options);
			DeleteOneProductService servicetest = new DeleteOneProductService(db);
			ProductEntity product = new ProductEntity()
			{
				Name = "Product Name",
				Price = 10,
				Description = "Description",
				AmountInStock = 2
			};

			db.Products.Add(product);
			await db.SaveChangesAsync();

			//Act
			await servicetest.DeleteAsync(product.ArticleNumber);
			var result = await db.Products.FirstOrDefaultAsync(x => x.ArticleNumber == product.ArticleNumber);

			//Assert
			Assert.Null(result);

		}
	}
}
