using ArrowInventory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public string CurrentPassword { get; set; } = "";
        [BindProperty]
        public string NewPassword { get; set; } = "";
        [BindProperty]
        public string ConfirmPassword { get; set; } = "";

        public ResetPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword) ||
                String.IsNullOrWhiteSpace(NewPassword) ||
                String.IsNullOrWhiteSpace(ConfirmPassword))
            {
                TempData["StatusMessage"] = "All fields are required";
                TempData["StatusType"] = "danger";
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("/Login");

            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

            if(result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["StatusMessage"] = "Password changed successfully";
                TempData["StatusType"] = "success";
                return RedirectToPage("/Index");

            }

            TempData["StatusMessage"] = string.Join(",", result.Errors.Select(e => e.Description));
            TempData["StatusType"] = "danger";
            return Page();
        }
    }

}
