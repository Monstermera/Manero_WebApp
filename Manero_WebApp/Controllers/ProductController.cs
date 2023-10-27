using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.ProductService;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
	public class ProductController : Controller
	{
        #region constructors & private fields
        private readonly DataContext _context;
        private readonly AddProductService _addProductService;
        //private readonly GetOneProductService _getOneProductService;
        //private readonly AddProductService _addProductService;
        //private readonly AddProductService _addProductService;

        public ProductController(AddProductService addproductService, DataContext context)
        {
            _context = context;
            _addProductService = addproductService;
        }

        #endregion


        public async Task<IActionResult> Index()
		{
            var tag1 = new TagsEntity { Id = 1, TagName = "sale" };

            var category1 = new CategoriesEntity {Id = 7, CategoryName = "women" };

            var color = new ColorsEntity {Id = 3, ColorName = "black" };

            var size = new SizesEntity {Id = 3, SizeName = "m" };


            ProductEntity entity = new()
            {
                Name = "Product1",
                Price = 1.99m,
                Description = "Description",
                Tags = new List<TagsEntity> { tag1 },
                Categories = new List<CategoriesEntity> { category1 },
                Colors = new List<ColorsEntity> { color },
                Sizes = new List<SizesEntity> {  size },

            };

            await _addProductService.AddAsync(entity);
			return View();
		}
	}
}
