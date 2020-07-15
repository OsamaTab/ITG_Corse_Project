using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTS.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "Admin".ToUpper();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                role.NormalizedName = "Employee".ToUpper();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByEmailAsync("admin@i.com").Result == null)
            {
                Employee user = new Employee
                {
                    UserName = "admin@i.com",
                    Email = "admin@i.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwe123/").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
     
    }
}
