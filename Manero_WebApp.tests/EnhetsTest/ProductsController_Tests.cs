using Manero_WebApp.Contexts;
using Manero_WebApp.Controllers;
using Manero_WebApp.Helpers.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Manero_WebApp.Tests.EnhetsTest;

public class ProductsController_Tests
{
    private readonly Mock<IDeleteOneProductService> _deleteOneProductService = new();
    private readonly DeleteProductController _deleteProductsController;

    public ProductsController_Tests()
    {
        
        _deleteProductsController = new DeleteProductController(_deleteOneProductService.Object);
    }

    [Fact]
    public async void Check_If_Returns_Redirect_To_AddOrEdit_When_No_Guid()
    {
        //Arrange
        var guid = Guid.NewGuid();
        _deleteOneProductService.Setup(x => x.DeleteAsync(guid)).ReturnsAsync(true);

        //Act
        var result = await _deleteProductsController.DeleteProduct(guid);

        //Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Products", viewResult.ControllerName);
        Assert.Equal("Index", viewResult.ActionName);
    }
}
