using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bunit;
using System.ComponentModel;
using Manero_WebApp.ViewModels.AccountViewModels;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.Tests.EnhetsTest
{
    public class ForgotPasswordTests : TestContext
    {
        private ForgotPasswordViewModel _viewModel;
        [Fact]
        public void ForgotPassword_ValidEmail_GoodValidation()
        {
            // Arrange 
            _viewModel = new ForgotPasswordViewModel();
            _viewModel.Email = "validemail@gmail.com";
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(_viewModel, null, null);

            // Act
            Validator.TryValidateObject(_viewModel, ctx, validationResults, true);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void ForgotPassword_EmptyEmail_BadValidation()
        {
            // Arrange 
            _viewModel = new ForgotPasswordViewModel();
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(_viewModel, null, null);

            // Act
            Validator.TryValidateObject(_viewModel, ctx, validationResults, true);

            // Assert
            Assert.Single(validationResults);
            Assert.Equal("You must provide an e-mail", validationResults[0].ErrorMessage );
        }

    }
}
