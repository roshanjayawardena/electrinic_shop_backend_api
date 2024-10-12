using Electronic_Application.Helpers;
using Electronic_Domain.Common.Enums;
using Electronic_Domain.Entities;
using Electronic_Infastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Electronic_Infastructure.Seed
{
    public class ElectronicContextSeed
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public ElectronicContextSeed(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SeedRolesAsync()
        {

            var roles = new List<ApplicationRole>
                {
                   new ApplicationRole("Designer"),
                   new ApplicationRole("Customer"),
                   new ApplicationRole("Supplier"),
                   new ApplicationRole("Admin"),
                };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                    await _roleManager.CreateAsync(role);
            };



            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new ApplicationRole
                {
                    Name = "Admin"
                };

                await _roleManager.CreateAsync(adminRole);
            }          
        }

        public async Task SeedAdminUserAsync()
        {
            var adminEmail = _configuration.GetSection("AdminUserEmail").Value;
            var adminPassword = _configuration.GetSection("AdminUserPassword").Value;

            //Super Admin User
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var businessUser = new BusinessUser()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Status = BusinessUserStatus.Active
                };
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    BusinessUser = businessUser
                };
                await _userManager.CreateAsync(adminUser, adminPassword);
                await _userManager.AddToRoleAsync(adminUser, RoleHelper.Admin);
            }

        }

        public static async Task SeedUserRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration config)
        {
            var roles = new List<ApplicationRole>
                {
                   new ApplicationRole("Designer"),
                   new ApplicationRole("Customer"),
                   new ApplicationRole("Supplier"),
                   new ApplicationRole("Admin"),
                };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                    await roleManager.CreateAsync(role);
            };

            var adminEmail = config.GetSection("AdminUserEmail").Value;
            var adminPassword = config.GetSection("AdminUserPassword").Value;

            //Admin User
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, RoleHelper.Admin);
            }
        }
    }
}
