using _3dd_Data.Models;
using _3dd_Data.Models.Product_dir;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Db
{
    public class DbContext3d : IdentityDbContext<MyAppUser,MyAppRole,int,IdentityUserClaim<int>,MyAppRoleUser,
        IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MyAppRoleUser>(ur =>
            {
                ur.HasKey(u => new { u.UserId, u.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.UserId)
                    .IsRequired();
            });
        }
    }
}
