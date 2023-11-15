using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<(IdentityResult result, string message)> AddAddressAsync(AddressViewModel model, UserEntity user)
    {
        var userResult = await _userManager.Users.Include(x => x.Addresses).SingleOrDefaultAsync(u => u.Id == user.Id);
        
        if (userResult != null)
        {
            var addressResult = await _addressDbRepo.GetAsync(x => x.City == model.City && x.PostalCode == model.PostalCode && x.StreetName == model.StreetName);
            
            if(addressResult != null)
            {
                if (userResult.Addresses.Contains(addressResult))
                {
                    return (IdentityResult.Success, "This address allready exists on your account.");
                }
                userResult.Addresses.Add(addressResult);
                return (await _userManager.UpdateAsync(userResult), "Address Created!");
            }
            else
            {
                AdressEntity adressEntity = model;
                var addressAdded = await _addressDbRepo.AddAsync(adressEntity);
                if (addressAdded != null)
                {
                    userResult.Addresses.Add(adressEntity);
                    return (await _userManager.UpdateAsync(userResult), "Address Created!");
                }
            }
        }
        return (null!, "The User was not found! Please contact IT service or try again.");
    }
}
