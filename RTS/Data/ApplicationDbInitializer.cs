using Microsoft.AspNetCore.Identity;
using RTS.DataAccess.Logic.RTSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTS.Data
{
    public class ApplicationDbInitializer
    {
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
