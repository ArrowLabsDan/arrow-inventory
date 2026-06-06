using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    /*[Authorize(Roles = "Admin")] -- Need a logout button before we can make this live*/ 
    public class AdminModel : PageModel
    {
        private readonly SiteService _siteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public List<Sites> Sites { get; set; } = [];

        [BindProperty]
        public string NewUsername { get; set; } = "";

        public List<ApplicationUser> Users { get; set; } = [];
        public Dictionary<string, IList<string>> UserRoles { get; set; } = new();

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

            foreach (var user in Users)
            {
                UserRoles[user.Id] = await _userManager.GetRolesAsync(user);
            }

        }


    }
}
