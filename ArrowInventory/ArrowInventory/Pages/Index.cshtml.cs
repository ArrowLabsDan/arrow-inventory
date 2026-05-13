using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class IndexModel : PageModel
    {

        private readonly DeviceService _deviceService;

        public List<Devices> Devices { get; set; } = [];

        public IndexModel(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }



        public void OnGet()
        {

            Devices = _deviceService.GetDevices();

        }


        public IActionResult OnPostDelete(string hostname)
        {
            _deviceService.DeleteDevice(hostname);
            return RedirectToPage();
        }

    }
}
