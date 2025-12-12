using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDataSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>()
                        {
                            new IdentityRole(){Name = "SuperAdmin"},
                            new IdentityRole(){Name = "Admin"},
                         };
                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name).Result)
                        {
                            var result = roleManager.CreateAsync(role).Result;
                        }
                    }
                }

                if (!userManager.Users.Any())
                {
                    var superAdmin = new ApplicationUser
                    {
                        FirstName = "Ahmed",
                        LastName = "Samir",
                        UserName = "ahmedsamir",
                        Email = "ahmed12@gmail.com",
                        PhoneNumber = "01234567890",
                    };

                    userManager.CreateAsync(superAdmin, "Password@123").Wait();
                    userManager.AddToRoleAsync(superAdmin, "SuperAdmin").Wait();
                    var admin = new ApplicationUser
                    {
                        FirstName = "Mohamed",
                        LastName = "Ali",
                        UserName = "mohamedali",
                        Email = "mohamed12@gmail.com",
                        PhoneNumber = "01345686534",
                    };

                    userManager.CreateAsync(admin, "Password@123").Wait();
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
