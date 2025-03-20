using Microsoft.AspNetCore.Identity;

namespace HealthApp.Razor.Data
{
    public static class HealthAppRoles
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
    }
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new string[] { HealthAppRoles.Patient, HealthAppRoles.Doctor, HealthAppRoles.Admin };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        Console.WriteLine($"Creating role {role}");
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                string adminUserName = "admin@healthapp.com";
                string adminUserEmail = "admin@healthapp.com";
                string genericPassword = "Letmein01*";

                if (await userManager.FindByEmailAsync(adminUserEmail) == null)
                {

                    var user = new IdentityUser { UserName = adminUserName, Email = adminUserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, genericPassword);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.Admin);
                }


                string doctorUserName = "doctor@healthapp.com";
                string doctorUserEmail = "doctor@healthapp.com";
               

                if (await userManager.FindByEmailAsync(doctorUserEmail) == null)
                {

                    var user = new IdentityUser { UserName = doctorUserName, Email = doctorUserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, genericPassword);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.Doctor);
                }

                string patientUserName = "patient@healthapp.com";
                string patientUserEmail = "patient@healthapp.com";


                if (await userManager.FindByEmailAsync(patientUserEmail) == null)
                {

                    var user = new IdentityUser { UserName = patientUserName, Email = patientUserEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(user, genericPassword);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.Patient);
                }

            }
        }
    }
}
