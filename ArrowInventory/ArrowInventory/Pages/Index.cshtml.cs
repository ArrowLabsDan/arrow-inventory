using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class IndexModel : PageModel
    {

        private readonly DeviceService _deviceService;
        private readonly SiteService _siteService;

        public List<Devices> Devices { get; set; } = [];
        public List<Sites> Sites { get; set; } = [];

        public IndexModel(DeviceService deviceService, SiteService siteService)
        {
            _deviceService = deviceService;
            _siteService = siteService;
        }



        public void OnGet()
        {

            Devices = _deviceService.GetDevices();
            Sites = _siteService.GetSites();

        }


        public IActionResult OnPostDelete(string hostname)
        {
            _deviceService.DeleteDevice(hostname);
            return RedirectToPage();
        }

    }
}
