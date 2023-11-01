using Manero_WebApp.Controllers;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Models.Schemas;
using Manero_WebApp.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manero_WebApp.Tests.EnhetsTest
{
    /// <summary>
    /// TESTER PÅ HOMECONTROLLER AV ELIAS
    /// </summary>
    public class HomeController_Tests
    {
        private readonly Mock<IGetAllProductsService> _mockGetAllProductsService;
        private readonly Mock<IGetOneProductService> _mockGetOneProductService;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<IRequestCookieCollection> _mockRequestCookies;
        private readonly Mock<IResponseCookies> _mockResponseCookies;
        private readonly HomeController _controller;

        public HomeController_Tests()
        {
            _mockGetAllProductsService = new Mock<IGetAllProductsService>();
            _mockGetOneProductService = new Mock<IGetOneProductService>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockRequestCookies = new Mock<IRequestCookieCollection>();
            _mockHttpContext.SetupGet(c => c.Request.Cookies).Returns(_mockRequestCookies.Object);
            _mockResponseCookies = new Mock<IResponseCookies>();
            _mockHttpContext.SetupGet(c => c.Response.Cookies).Returns(_mockResponseCookies.Object);

            _controller = new HomeController(_mockGetAllProductsService.Object, _mockGetOneProductService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };
        }


        /// <summary>
        /// <para>Index_FirstVisit_ReturnsWelcomeOnboardingView:</para>
        /// <para>Syfte: Att kontrollera om förstagångsbesökare visas vyn "WelcomeOnboarding".</para>
        /// <para>Metod: Den ställer in scenariot där "Besökt"-cookien saknas och anropar sedan Indexmetoden.</para>
        /// <para>Förväntat resultat: Den returnerade vyn ska vara "WelcomeOnboarding".</para>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_FirstVisit_ReturnsWelcomeOnboardingView()
        {
            // Arrange
            _mockRequestCookies.Setup(c => c.ContainsKey("Visited")).Returns(false);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("WelcomeOnboarding", viewResult.ViewName);
        }

        /// <summary>
        /// <para>Index_NotFirstVisit_ReturnsHomePageViewModelView:</para>
        /// <para>Syfte: Att verifiera att återkommande besökare inte visas introduktionsvyn igen.</para>
        /// <para>Metod: Den ställer in scenariot där "Besökt"-cookien finns och anropar sedan Indexmetoden.</para>
        /// <para>Förväntat resultat: Standardvyn utan en ViewModel ska returneras.</para>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_NotFirstVisit_ReturnsHomePageViewModelView()
        {
            // Arrange
            _mockRequestCookies.Setup(c => c.ContainsKey("Visited")).Returns(true);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);  // For default View without ViewModel
        }

        /// <summary>
        /// <para>ProductDetails_ValidArticleNumber_ReturnsProduct:</para>
        /// <para>Syfte: Att bekräfta att korrekt produktinformation visas när den förses med ett giltigt artikelnummer.</para>
        /// <para>Metod: En låtsasprodukt med ett unikt artikelnummer ställs in, och sedan anropas metoden ProductDetails med det artikelnumret.</para>
        /// <para>Förväntat resultat: Den returnerade vyn bör visa detaljerna för låtsasprodukten.</para>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProductDetails_ValidArticleNumber_ReturnsProduct()
        {
            // Arrange
            var sampleProduct = new ProductModel { ArticleNumber = Guid.NewGuid() };
            _mockGetOneProductService.Setup(s => s.GetOneProductAsync(sampleProduct.ArticleNumber)).ReturnsAsync(sampleProduct);

            // Act
            var result = await _controller.ProductDetails(sampleProduct.ArticleNumber);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var product = Assert.IsType<ProductModel>(viewResult.Model);
            Assert.Equal(sampleProduct.ArticleNumber, product.ArticleNumber);
        }

        /// <summary>
        /// <para>ProductDetails_InvalidArticleNumber_ReturnsNotFound:</para>
        /// <para>Syfte: Att säkerställa att om någon försöker se en produkt med ett ogiltigt artikelnummer får de ett "hittad ej"-svar.</para>
        /// <para>Metod: Den sätter upp ett scenario där GetOneProductAsync-tjänsten inte returnerar någon produkt för ett givet artikelnummer och anropar sedan ProductDetails-metoden.</para>
        /// <para>Förväntat resultat: Resultatet bör vara ett "NotFound"-svar.</para>
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProductDetails_InvalidArticleNumber_ReturnsNotFound()
        {
            // Arrange
            var invalidArticleNumber = Guid.NewGuid();
            _mockGetOneProductService.Setup(s => s.GetOneProductAsync(invalidArticleNumber)).ReturnsAsync((ProductModel)null);

            // Act
            var result = await _controller.ProductDetails(invalidArticleNumber);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
