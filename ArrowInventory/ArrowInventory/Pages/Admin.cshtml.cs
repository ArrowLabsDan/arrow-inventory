using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class AdminModel : PageModel
    {
        private readonly SiteService _siteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public string SiteCode { get; set; } = "";
        [BindProperty]
        public string SiteName { get; set; } = "";
        public List<Sites> Sites { get; set; } = [];

        [BindProperty]
        public string NewUsername { get; set; } = "";
        [BindProperty]
        public string NewPassword { get; set; } = "";
        [BindProperty]
        public string NewDisplayName { get; set; } = "";
        [BindProperty]
        public string NewRole { get; set; } = "ReadOnly";
        public List<ApplicationUser> Users { get; set; } = [];

        public string StatusMessage { get; set; } = "";
        public string StatusType { get; set; } = "";

        public AdminModel(SiteService siteService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _siteService = siteService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            Sites = _siteService.GetSites();
            Users = _userManager.Users.ToList();
        }

        public IActionResult OnPost()
        {
            Sites = _siteService.GetSites();

            if (string.IsNullOrWhiteSpace(SiteName))
            {
                TempData["StatusMessage"] = "Site Name Cannot be empty";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

            if (string.IsNullOrWhiteSpace(SiteCode))
            {
                TempData["StatusMessage"] = "Site Code Cannot be empty";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }


            if (Sites.Any(x => x.SiteName.ToLower() == SiteName.ToLower()))
            {
                TempData["StatusMessage"] = $"{SiteName} already exitst under sites";
                TempData["StatusType"] = "warning";
                return RedirectToPage();
            }

            if (Sites.Any(x => x.SiteCode.ToLower() == SiteCode.ToLower()))
            {
                TempData["StatusMessage"] = $"SiteCode - {SiteCode} is already in use";
                TempData["StatusType"] = "warning";
                return RedirectToPage();
            }

            _siteService.AddSite(new Sites
            {
                SiteName = SiteName,
                SiteCode = SiteCode
            });


            TempData["StatusMessage"] = $"{SiteName} ({SiteCode}) added successfully";
            TempData["StatusType"] = "success";


            return RedirectToPage();
        }


        public IActionResult OnPostDelete(string siteCode)
        {
            _siteService.DeleteSite(siteCode);
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostCreateUserAsync()
        {
            Sites = _siteService.GetSites();
            Users = _userManager.Users.ToList();

            if (String.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
            {
                TempData["StatusMessage"] = "Username and password are required";
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

            /* Commented out for now - Will add when auth is enforced
            if (user.UserName == User.Identity?.Name)
            {
                TempData["StatusMessage"] = "You cannot delete your own account";
                TempData["StatusType"] = "danger";
            }

            */
            return RedirectToPage();
        }


    }
}
