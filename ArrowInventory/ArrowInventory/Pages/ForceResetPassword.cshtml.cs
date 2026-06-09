using ArrowInventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class ForceResetPasswordModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public string newPassword { get; set; }= "";
        [BindProperty]
        public string confirmPassword { get; set; } = "";

        public ForceResetPasswordModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToPage("/Login");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                TempData["StatusMessage"] = "Both password fields are required";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            if (newPassword != confirmPassword)
            {
                TempData["StatusMessage"] = "Passwords do not match";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Login");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                user.PasswordChangeDate = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage("/Index");
            }

            TempData["StatusMessage"] = string.Join(",", result.Errors.Select(e => e.Description));
            TempData["StatusType"] = "danger";
            return Page();
        }
    }
}
