

using Manero_WebApp.ViewModels.AccountViewModels;

namespace Manero_WebApp.Tests.EnhetsTest;

public class AddressViewModel_Tests
{
    [Fact]
    public void Cast_AddressViewModel_To_AdressEntity()
    {
        //Arrange
        AddressViewModel addressViewModel = new()
        {
            StreetName = "streetname",
            PostalCode = "12345",
            City = "City"
        };

        //Act
        AdressEntity addressEntity = addressViewModel;

        //Assert
        Assert.IsType<AdressEntity>(addressEntity);
        Assert.Equal(addressEntity.StreetName, addressViewModel.StreetName);
        Assert.Equal(addressEntity.PostalCode, addressViewModel.PostalCode);
        Assert.Equal(addressEntity.City, addressViewModel.City);
    }
}
