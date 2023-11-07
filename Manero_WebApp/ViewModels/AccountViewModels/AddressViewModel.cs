using Manero_WebApp.Models.Entities;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace Manero_WebApp.ViewModels.AccountViewModels;

public class AddressViewModel
{
    [Display(Name = "Streetname*")]
    [Required(ErrorMessage = "Please fill in a streetname.")]
    public string StreetName { get; set; } = null!;

    [Display(Name = "Postalcode*")]
    [Required(ErrorMessage = "Please fill in a postalcode.")]
    [RegularExpression(@"^(?:\d{1,6}|\d{0,5}\s|\s\d{0,5}|\d{1,5}\s\d)$", ErrorMessage = "The postalcode should only contain max 6 digits")]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City*")]
    [Required(ErrorMessage = "Please fill in a city.")]
    public string City { get; set; } = null!;

    public static implicit operator AdressEntity(AddressViewModel model)
    {
        return new AdressEntity
        {
            StreetName = model.StreetName,
            PostalCode = model.PostalCode,
            City = model.City,
        };
    }
}
