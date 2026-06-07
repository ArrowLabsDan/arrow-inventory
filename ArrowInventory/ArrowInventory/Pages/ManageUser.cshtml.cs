using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ArrowInventory.Pages
{

    public class ManageUserModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public string NewUsername { get; set; } = "";
        [BindProperty]
        public string NewPassword { get; set; } = "";
        [BindProperty]
        public string NewDisplayName { get; set; } = "";
        [BindProperty]
        public string NewRole { get; set; } = "ReadOnly";
        [BindProperty]
        public string NewEmail { get; set; } = "";
        public List<ApplicationUser> Users { get; set; } = [];
        public Dictionary<string, IList<string>> UserRoles { get; set; } = new();


        public ManageUserModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();

            foreach (var user in Users)
            {
                UserRoles[user.Id] = await _userManager.GetRolesAsync(user);
            }

        }

        public async Task<IActionResult> OnPostCreateUserAsync()
        {
            Users = _userManager.Users.ToList();

            if (String.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
            {
                TempData["StatusMessage"] = "Username and password are required";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            if (String.IsNullOrWhiteSpace(NewDisplayName))
            {
                TempData["StatusMessage"] = "DisplayName must be entered";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            if(Users.Any(x => x.DisplayName?.ToLower() == NewDisplayName.ToLower()))
            {
                TempData["StatusMessage"] = $"{NewDisplayName} already exitst in the inventory";
                TempData["StatusType"] = "warning";
                return RedirectToPage();
            }

            if (String.IsNullOrWhiteSpace(NewEmail))
            {
                TempData["StatusMessage"] = "Email must be entered";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            foreach (var role in new[] { "Admin", "Read & Write", "Read Only" })
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }


            var user = new ApplicationUser
            {
                UserName = NewUsername,
                DisplayName = NewDisplayName,
                Email = NewEmail,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, NewPassword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, NewRole);
                TempData["StatusMessage"] = $"{NewUsername} created succesfully";
                TempData["StatusType"] = "success";
            }
            else
            {
                TempData["StatusMessage"] = string.Join(", ", result.Errors.Select(e => e.Description));
                TempData["StatusType"] = "danger";
            }

            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userID)
        {

            var user = await _userManager.FindByIdAsync(userID);
            if (user != null)
                await _userManager.DeleteAsync(user);
          
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditUserAsync(string userID, string displayName, string email, string role)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
            {
                TempData["StatusMessage"] = "User not found";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            user.DisplayName = displayName;
            user.Email = email;
            await _userManager.UpdateAsync(user);

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role);

            TempData["StatusMessage"] = $"{user.UserName} updated successfully";
            TempData["StatusType"] = "success";

            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostResetUserPasswordAsync(string userId, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
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

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["StatusMessage"] = "User not found";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                TempData["StatusMessage"] = $"{user.DisplayName} password reset successfully";
                TempData["StatusType"] = "success";
            }
            else
            {
                TempData["StatusMessage"] = string.Join(",", result.Errors.Select(e => e.Description));
                TempData["StatusType"] = "danger";
            }

            return RedirectToPage();

        }


    }
}