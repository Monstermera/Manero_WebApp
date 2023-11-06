using Manero_WebApp.Helpers.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Controllers
{
    public class DeleteProductController : Controller
    {
        private readonly IDeleteOneProductService _deleteOneProductService;

        public DeleteProductController(IDeleteOneProductService deleteOneProductService)
        {
            _deleteOneProductService = deleteOneProductService;
        }


        //Delete Product
        public async Task<IActionResult> DeleteProduct(Guid data)
        {
            var result = await _deleteOneProductService.DeleteAsync(data);
            if (result == true)
            {
                return RedirectToAction("Index", "Products");
            }
            return RedirectToAction("AddOrEdit" , "Products", new { Id = data });
        }
    }
}
