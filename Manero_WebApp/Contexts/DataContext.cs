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
    public DbSet<CategoriesEntity> Categories { get; set; }
    public DbSet<ColorsEntity> Colors { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<ProductColorsEntity> ProductColors { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductImageUrlEntity> ProductImages { get; set; }
    public DbSet<ProductSizesEntity> ProductSizes { get; set; }
    public DbSet<ProductTagsEntity> ProductTags { get; set; }
    public DbSet<ReviewsEntity> Reviews { get; set; }
    public DbSet<SizesEntity> Sizes { get; set; }
    public DbSet<TagsEntity> Tags { get; set; }

}
