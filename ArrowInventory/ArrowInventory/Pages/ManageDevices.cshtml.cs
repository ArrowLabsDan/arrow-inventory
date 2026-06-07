using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

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

        public IActionResult OnGetExport()
        {
            var devices = _deviceService.GetDevices();
            var csv = new StringBuilder();
            csv.AppendLine("Hostname,SiteCode,SerialNumber,Model,IP,MACAddress,IsVirtualMachine,Locatiom,CPU,RAM,Storage,OS,Description");

            foreach (var d in devices)
                csv.AppendLine($"{d.Hostname},{d.SiteCode},{d.SerialNumber},{d.Model},{d.IP},{d.MACAddress},{d.isVirtualMachine},{d.location},{d.CPU},{d.RAM},{d.Storage},{d.OS},{d.description}");

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "devices.csv");
        }


        public IActionResult OnPostEdit(string hostname, string serialNumber, string model, bool isVirtualMachine, string ip, string description, string location, string cpu, string ram, string storage, string macaddress, string os, string sitecode)
        {
            if (string.IsNullOrWhiteSpace(sitecode))
            {
                TempData["StatusMessage"] = "Site cannot be empty";
                TempData["StatusType"] = "danger";
                return RedirectToPage();
            }

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
