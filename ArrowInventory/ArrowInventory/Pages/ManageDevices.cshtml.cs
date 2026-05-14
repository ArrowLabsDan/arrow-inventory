using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class ManageDevicesModel : PageModel
    {

        private readonly DeviceService _deviceService;
        private readonly SiteService _siteService;

        public List<Devices> Devices { get; set; } = [];
        public List<Sites> Sites { get; set; } = [];

        public ManageDevicesModel(DeviceService deviceService, SiteService siteService)
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

        public IActionResult OnPostEdit(string hostname, string serialNumber, string model, bool isVirtualMachine, string ip, string description, string location, string cpu, string ram, string storage, string macaddress, string os, string sitecode)
        {
            var updated = new Devices
            {
                Hostname = hostname,
                SiteCode = sitecode,
                SerialNumber = serialNumber,
                Model = model,
                isVirtualMachine = isVirtualMachine,
                IP = ip,
                description = description,
                location = location,
                CPU = cpu,
                RAM = ram,
                Storage = storage,
                MACAddress = macaddress,
                OS = os

            };

            _deviceService.UpdateDevices(updated);
            return RedirectToPage();
        }

    }
}
