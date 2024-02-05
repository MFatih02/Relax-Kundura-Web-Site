using Microsoft.AspNetCore.Identity;
using RelaxKundura3.Models;

namespace RelaxKundura3.Data
{
	public class DbSeeder
	{
		public static async Task SeedDefaultData(IServiceProvider service) 
		{
			var UserMgr = service.GetService<UserManager<IdentityUser>>();
			var RoleMgr = service.GetService<RoleManager<IdentityRole>>();

			await RoleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await RoleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

			var Admin = new IdentityUser
			{
				UserName = "Admin@gmail.com",
				Email = "Admin@gmail.com",
				EmailConfirmed = true
			};

			var UserInDb = await UserMgr.FindByEmailAsync(Admin.Email);
			if (UserInDb is null) 
			{
				await UserMgr.CreateAsync(Admin, "Admin_1234");
				await UserMgr.AddToRoleAsync(Admin,Roles.Admin.ToString());
			}

		}
	}
}
