using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class ManageDevicesModel : PageModel
    {

        private readonly DeviceService _deviceService;

        public List<Devices> Devices { get; set; } = [];

        public ManageDevicesModel(DeviceService deviceService)
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

        public IActionResult OnPostEdit(string hostname, string serialNumber, string model, bool isVirtualMachine, string ip, string description, string location)
        {
            var updated = new Devices
            {
                Hostname = hostname,
                SerialNumber = serialNumber,
                Model = model,
                isVirtualMachine = isVirtualMachine,
                IP = ip,
                description = description,
                location = location
            };

            _deviceService.UpdateDevices(updated);
            return RedirectToPage();
        }

    }
}
