using Microsoft.AspNetCore.Identity;
using ClassroomReservation.Models;

namespace ClassroomReservation.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserService> _logger;

        public UserService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<(bool success, string message)> CreateUserAsync(string email, string password)
        {
            try
            {
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password");
                    return (true, "User created successfully");
                }

                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return (false, "An error occurred while creating the user");
            }
        }

        public async Task<(bool success, string message)> ValidateCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return (false, "Invalid login attempt");
                }

                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in");
                    return (true, "Login successful");
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out");
                    return (false, "Account locked out");
                }

                return (false, "Invalid login attempt");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return (false, "An error occurred during login");
            }
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }
    }
}
