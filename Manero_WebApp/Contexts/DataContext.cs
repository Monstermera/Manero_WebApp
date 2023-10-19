using Manero_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Manero_WebApp.Contexts;

public class DataContext : IdentityDbContext<UserEntity>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    //Add DbSet here
    public DbSet<AdressEntity> Adresses { get; set; }
    public DbSet<UserAdressEntity> UserAdresses { get; set; }



}
