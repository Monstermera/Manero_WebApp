using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace Manero_WebApp.Helpers.Services.UserServices;

public class AddressService 
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AdressDbRepo _addressDbRepo;

    public AddressService(UserManager<UserEntity> userManager, AdressDbRepo addressDbRepo)
    {
        _userManager = userManager;
        _addressDbRepo = addressDbRepo;
    }

    public async Task<(IdentityResult result, string message)> AddAddressAsync(AddressViewModel model)
    {
        var userResult = await _userManager.FindByIdAsync(model.UserEntity.Id);
        if (userResult != null)
        {
            AdressEntity adressEntity = model;
            var addressResult = await _addressDbRepo.GetAsync(x => x.City == adressEntity.City && x.PostalCode == adressEntity.PostalCode && x.StreetName == adressEntity.StreetName);
            
            if(addressResult != null)
            {
                userResult.Addresses.Add(addressResult);
                return (await _userManager.UpdateAsync(userResult), "Address Created!");
            }
            else
            {
                var addressAdded = await _addressDbRepo.AddAsync(adressEntity);
                if (addressAdded != null)
                {
                    userResult.Addresses.Add(addressResult);
                    return (await _userManager.UpdateAsync(userResult), "Address Created!");
                }
            }
            
        }
        return (null!, "The User was not found! Please contact IT service or try again.");
    }
}
