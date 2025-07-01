using Microsoft.AspNetCore.Identity;

namespace ClassroomReservation.Services
{
    public class IdentityDataInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Create roles if they don't exist
                string[] roles = { "Admin", "Instructor" };
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                        _logger.LogInformation($"Created role: {role}");
                    }
                }

                // Create admin user if it doesn't exist
                var adminUser = await _userManager.FindByNameAsync("admin");
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@classroom.com",
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(adminUser, "Admin123!");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "Admin");
                        _logger.LogInformation("Created admin user");
                    }
                    else
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        _logger.LogError($"Failed to create admin user: {errors}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while initializing identity data");
                throw;
            }
        }

        public async Task CreateInstructorUserAsync(string email, string password)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Instructor");
                    _logger.LogInformation($"Created instructor user: {email}");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError($"Failed to create instructor user: {errors}");
                    throw new Exception($"Failed to create instructor user: {errors}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while creating instructor user: {email}");
                throw;
            }
        }
    }
}
