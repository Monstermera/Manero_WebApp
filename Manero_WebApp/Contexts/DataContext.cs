using Manero_WebApp.Models;
using Manero_WebApp.Models.Entities;
using Manero_WebApp.Models.Schemas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Manero_WebApp.Contexts
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        // DbSet definitions here
        public DbSet<AdressEntity> Adresses { get; set; }
        public DbSet<CategoriesEntity> Categories { get; set; }
        public DbSet<ColorsEntity> Colors { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductImageUrlEntity> ProductImages { get; set; }
        public DbSet<ReviewsEntity> Reviews { get; set; }
        public DbSet<SizesEntity> Sizes { get; set; }
        public DbSet<TagsEntity> Tags { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    ArticleNumber = 1,
                    Name = "Socka",
                    Price = 15,
                    Description = "Hej",
                },
                new ProductEntity
                {
                    ArticleNumber = 2,
                    Name = "Tröja",
                    Price = 150,
                    Description = "Hej",
                }
            );
        }
    }
    
}


