using ArrowInventory.Services;
using ArrowInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class AddDevicesModel : PageModel
    {

        private readonly DeviceService _deviceService;

        [BindProperty]
        public string Hostname { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public AddDevicesModel(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public void OnPost()
        {
            var devices = _deviceService.GetDevices();
            devices.Add(new Devices
            {
                Hostname = Hostname
            });

            _deviceService.SaveDevices(devices);

            SuccessMessage = $"{Hostname} added successfully";

            // Clear UI state + reset form
            ModelState.Clear();
            Hostname = "";
        }
    }
}
