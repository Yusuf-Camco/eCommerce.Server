using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Archive> CheckOutArchives { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Credit Card"
                });
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                },
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "USER",
                     NormalizedName = "USER"
                 }
                );


        }
    }


    
}
