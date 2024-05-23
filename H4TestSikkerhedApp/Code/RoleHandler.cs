using H4TestSikkerhedApp.Data;
using Microsoft.AspNetCore.Identity;

namespace H4TestSikkerhedApp.Code
{
	public class RoleHandler
	{
        public async Task CreateUserRoles(string user, string role, IServiceProvider _serviceProvider)
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var userRoleCheck = await roleManager.RoleExistsAsync(role);
            if (!userRoleCheck)
                await roleManager.CreateAsync(new IdentityRole(role));

            ApplicationUser identityUser = await userManager.FindByEmailAsync(user);
            await userManager.AddToRoleAsync(identityUser, role);
        }
    }
}