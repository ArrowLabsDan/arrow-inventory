using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using Windows.Networking;

namespace ArrowInventory.Pages
{
    public class AdminModel : PageModel
    {
        private readonly SiteService _siteService;

        [BindProperty]
        public string SiteCode { get; set; } = "";
        [BindProperty]
        public string SiteName { get; set; } = "";
        public List<Sites> Sites { get; set; } = [];

        public string StatusMessage { get; set; } = "";
        public string StatusType { get; set; } = "";

        public AdminModel(SiteService siteService)
        {
            _siteService = siteService;
        }



        public void OnGet()
        {
            Sites = _siteService.GetSites();
        }

        public void OnPost()
        {
            Sites = _siteService.GetSites();

            if (string.IsNullOrWhiteSpace(SiteName))
            {
                StatusMessage = "Site Name Cannot be empty";
                StatusType = "danger";
                return;
            }

            if (string.IsNullOrWhiteSpace(SiteCode))
            {
                StatusMessage = "Site Code Cannot be empty";
                StatusType = "danger";
                return;
            }

           
            if (Sites.Any(x => x.SiteName.ToLower() == SiteName.ToLower()))
            {
                StatusMessage = $"{SiteName} already exitst in the inventory";
                StatusType = "warning";
                return;
            }

            if (Sites.Any(x => x.SiteCode.ToLower() == SiteCode.ToLower()))
            {
                StatusMessage = $"{SiteCode} already exitst in the inventory";
                StatusType = "warning";
                return;
            }

            Sites.Add(new Sites
            {
                SiteName = SiteName,
                SiteCode = SiteCode
            });
           

            _siteService.SaveSites(Sites);

           

            StatusMessage = $"{SiteName} ({SiteCode}) added successfully";
            StatusType = "success";

            // Clear UI state + reset form
            ModelState.Clear();
            SiteCode = "";
            SiteName = "";



        }

    }
}
