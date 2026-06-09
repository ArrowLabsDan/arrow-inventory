using ArrowInventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Windows.Networking;

namespace ArrowInventory.Pages
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public string Username { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";
        [BindProperty]
        public bool RememberMe { get; set; } = false;
        public string ErrorMessage { get; set; } = "";


        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (String.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                TempData["StatusMessage"] = "Username and password are required.";
                TempData["StatusType"] = "danger";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Username, Password, RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var loggedInUser = await _userManager.FindByNameAsync(Username);
                if (loggedInUser?.PasswordChangeDate == null ||
                    loggedInUser.PasswordChangeDate < DateTime.UtcNow.AddDays(-31))
                {
                    TempData["ForceReset"] = true;
                    return RedirectToPage("/ForceResetPassword");
                }

                return RedirectToPage("/Index");
            }


            TempData["StatusMessage"] = "Invalid Username or Password";
            TempData["StatusType"] = "danger";

            return Page();



        }




    }
}
