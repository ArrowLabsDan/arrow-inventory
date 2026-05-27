using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
           

            _siteService.SaveSites(Sites);



            TempData["StatusMessage"] = $"{SiteName} ({SiteCode}) added successfully";
            TempData["StatusType"] = "success";


            return RedirectToPage();
        }


        public IActionResult OnPostDelete(string siteCode)
        {
            _siteService.DeleteSite(siteCode);
            return RedirectToPage();
        }

    }
}
