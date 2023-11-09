using Manero_WebApp.Controllers;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Tests.EnhetsTest
{
    /// <summary>
    /// TESTER PÅ WISHLISTCONTROLLER AV DIMET
    /// </summary>
    public class WishlistController_Tests
    {
        private readonly Mock<WishlistRepo> _mockWishlistRepo;
        private readonly Mock<IGetOneProductService> _mockGetOneProductService;
        private readonly Mock<WishlistProductService> _mockWishlistProductService;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly WishlistController _controller;

        public WishlistController_Tests()
        {
            _mockGetOneProductService = new Mock<IGetOneProductService>();
            _mockWishlistRepo = new Mock<WishlistRepo>();
            _mockWishlistProductService = new Mock<WishlistProductService>(_mockWishlistRepo.Object, _mockGetOneProductService.Object);
            _mockHttpContext = new Mock<HttpContext>();

            _controller = new WishlistController(_mockWishlistProductService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }

        /// <summary>
        /// Tests that adding a new product actually adds it to the wishlist.
        /// </summary>
        [Fact]
        public async Task Add_NewProduct_AddsItToWishlist()
        {
            var fakeProduct = new ProductModel
            { 
                ArticleNumber = Guid.NewGuid()
            };

            _mockGetOneProductService.Setup(i => i.GetOneProductAsync(It.IsAny<Guid>())).ReturnsAsync(fakeProduct);

            var response = await _controller.Add(fakeProduct.ArticleNumber);

            var okResponse = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(fakeProduct.ArticleNumber, okResponse.Value);
        }
    }
}
