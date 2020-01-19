using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public static class SeedIdentityData
    {
        private const string ADMIN_USERNAME = "Admin";
        private const string ADMIN_PASSWORD = "Secret123$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(ADMIN_USERNAME);

            if (user == null)
            {
                user = new IdentityUser(ADMIN_USERNAME);

                await userManager.CreateAsync(user, ADMIN_PASSWORD);
            }
        }
    }
}
