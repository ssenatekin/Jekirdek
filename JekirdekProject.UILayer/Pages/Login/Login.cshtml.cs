using JekirdekProject.DataAccessLayer.Repository;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;
using JekirdekProject.UILayer.ViewModels;

namespace JekirdekProject.UILayer.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IUserRepository _userRepository; 
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(IUserRepository userRepository, ILogger<LoginModel> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userRepository.ValidateUser(Input.Username, Input.Password);
            if (user == null) 
            {
                _logger.LogWarning("Login attempt failed for user {Username} at {Time}", Input.Username, DateTime.UtcNow);
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                //return Page();
            } else      
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role) 
            };
                _logger.LogInformation("User {Username} successfully logged in at {Time}", Input.Username, DateTime.UtcNow);
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Customers/Index"); 
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanýcý adý veya þifre.");
            return Page();
        }
    }
}
