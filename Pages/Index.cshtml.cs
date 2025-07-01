using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassroomReservation.Models;
using ClassroomReservation.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using ClassroomReservation.Services;

namespace ClassroomReservation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ClassroomDbContext _context;
        private readonly UserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ClassroomDbContext context,
            UserService userService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger)
        {
            _context = context;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public async Task<IActionResult> OnPostInstructorLogin()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return Page();
            }

            
            var instructor = _context.Instructors.FirstOrDefault(i => i.Email == Email);
            if (instructor == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email. Please use a registered email.");
                return Page();
            }

          
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Authentication failed. Please contact administrator.");
                _logger.LogError($"Identity user not found for instructor email: {Email}");
                return Page();
            }

           
            var result = await _signInManager.PasswordSignInAsync(user, Password, isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                {
                    _logger.LogInformation("Instructor logged in successfully");
                    HttpContext.Session.SetString("InstructorEmail", Email);
                    return RedirectToPage("/Instructor/Panel");
                }
                else
                {
                    
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError(string.Empty, "This account does not have instructor access.");
                    return Page();
                }
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Instructor account locked out");
                ModelState.AddModelError(string.Empty, "Account locked out. Please try again later.");
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        public async Task<IActionResult> OnPostAdminLoginAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required.");
                return Page();
            }            var result = await _signInManager.PasswordSignInAsync(Username, Password, isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(Username);
                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    _logger.LogInformation("Admin logged in successfully");
                    return RedirectToPage("/Admin/Panel");
                }
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Admin account locked out");
                ModelState.AddModelError(string.Empty, "Account locked out. Please try again later.");
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
