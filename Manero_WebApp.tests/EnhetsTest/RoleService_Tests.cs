﻿using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class RolesServiceTests
{
    [Fact]
    public async Task CreateAdminAndUserRoles()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "in_memory_db")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        using (var context = new DataContext(options))
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(
                roleStore,
                null,  
                null,  
                null,   
                null 
            );
            var userManager = new UserManager<UserEntity>(new UserStore<UserEntity>(context), null, null, null, null, null, null, null, null);

            var rolesService = new RolesService(userManager, roleManager);

            // Act
            await rolesService.AddRoleAsync(new UserEntity());

            // Assert
            Assert.True(await roleManager.RoleExistsAsync("admin"));
            Assert.True(await roleManager.RoleExistsAsync("customer"));
        }
    }
}
