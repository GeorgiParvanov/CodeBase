namespace CodeBase.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using CodeBase.Common;
    using CodeBase.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                UserName = GlobalConstants.AdministratorUsername,
                Email = GlobalConstants.AdministratorEmail,
                EmailConfirmed = true,
            };

            var userExists = await userManager.FindByEmailAsync(user.Email);
            if (userExists == null)
            {
                var adminUser = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);
                if (adminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
