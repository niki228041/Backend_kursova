using _3dd_Api_kusova;
using _3dd_Data.Models;
using _3dd_Data.Models.Product_dir;
using _3dd_Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.DbSeeder
{
    public static class Seeder
    {
        public static async void SeedData(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MyAppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<MyAppRole>>();
                var productManager = scope.ServiceProvider.GetRequiredService<IProductRepository>();


                if (!productManager.Products.Any())
                {
                    var newProduct = new Product
                    {
                        Name = "Нил",
                        UserId = 2,
                        DateCreated = DateTime.UtcNow,
                        IsDelete = false,
                        UploadDate = DateTime.UtcNow,
                        Extension = "none",
                        Size = "23mb",
                        InWhichPrograms = "blender",
                        CompanyId= 2,
                        LicenseType = "normal",
                        Images = new List<ProductImage>(),
                    };

                    await productManager.Create(newProduct);
                }

                if (!roleManager.Roles.Any())
                {
                    var result = roleManager.CreateAsync(new MyAppRole { Name=Roles.Admin}).Result;


                    result = roleManager.CreateAsync(new MyAppRole { Name=Roles.User}).Result;
                }

                if(!userManager.Users.Any())
                {
                    string admin = "admin@gmail.com";
                    var adminEntity = new MyAppUser 
                    { 
                        Name="Нил", 
                        Surname = "Алор",
                        Email = admin,
                        UserName= admin,
                    };

                    var result = userManager.CreateAsync(adminEntity,"123456").Result;
                    result = userManager.AddToRoleAsync(adminEntity, Roles.Admin).Result;
                }

                
            }
        }
    }
}
