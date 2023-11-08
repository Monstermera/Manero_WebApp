using Manero_WebApp.Contexts;
using Manero_WebApp.Helpers.Repositories;
using Manero_WebApp.Helpers.Services.AuthenticationServices;
using Manero_WebApp.Helpers.Services.ProductServices;
using Manero_WebApp.Helpers.Services.UserServices;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
}
);


//Repos
builder.Services.AddScoped<UserDbRepo>();
builder.Services.AddScoped<ProductDbRepo>();
builder.Services.AddScoped<AdressDbRepo>();

//Products
builder.Services.AddScoped<AddProductService>();
builder.Services.AddScoped<IGetOneProductService, GetOneProductService>();
builder.Services.AddScoped<IGetAllProductsService, GetAllProductsService>();
builder.Services.AddScoped<DeleteOneProductService>();
builder.Services.AddScoped<GetAllProductsService>();


//Services
builder.Services.AddScoped<UpdateProductService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<CheckIfUserExistsService>();
builder.Services.AddScoped<RolesService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AddressService>();

//Identity
builder.Services.AddIdentity<UserEntity, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>().AddRoleManager<RoleManager<IdentityRole>>().AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/Login";
    x.LogoutPath = "/";
    x.AccessDeniedPath = "/denied";
});

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "812808685205-is14h61h37h0s63l7nm6a08446n932hf.apps.googleusercontent.com";
    x.ClientSecret = "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";
    x.CallbackPath = "/signin-google";
    x.ClaimActions.MapJsonKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri", "picture");
});





var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/PageNotFound/Index");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
