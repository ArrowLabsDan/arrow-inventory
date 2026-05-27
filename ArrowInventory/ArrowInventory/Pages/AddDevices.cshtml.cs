using ArrowInventory.Models;
using ArrowInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArrowInventory.Pages
{
    public class AddDevicesModel : PageModel
    {

        private readonly DeviceService _deviceService;
        private readonly SiteService _siteService;

        // Form fields
        [BindProperty]
        public string Hostname { get; set; } = "";
        [BindProperty]
        public string SerialNumber { get; set; } = "";
        [BindProperty]
        public string Model { get; set; } = "";
        [BindProperty]
        public bool isVirtualMachine { get; set; } = false;
        [BindProperty]
        public string IP { get; set; } = "";
        [BindProperty]
        public string description { get; set; } = "";
        [BindProperty]
        public string location { get; set; } = "";
        [BindProperty]
        public string CPU { get; set; } = "";
        [BindProperty]
        public string RAM { get; set; } = "";
        [BindProperty]
        public string Storage { get; set; } = "";
        [BindProperty]
        public string MACAddress { get; set; } = "";
        [BindProperty]
        public string OS { get; set; } = "";

        // Drop down for Sites
        public List<Sites> Sites { get; set; } = [];

        [BindProperty]
        public string SiteCode { get; set; } = "";

        // UI Feedback properties
        public string StatusMessage { get; set; } = "";
        public string StatusType { get; set; } = "";

        public AddDevicesModel(DeviceService deviceService, SiteService siteService)
        {
            _deviceService = deviceService;
            _siteService = siteService;
            
        }

        public void OnGet()
        {
            Sites = _siteService.GetSites();
        }

        public IActionResult OnPost()
        {

            Sites = _siteService.GetSites();
            var devices = _deviceService.GetDevices();

            // Validates if hostname is empty
            if (string.IsNullOrWhiteSpace(Hostname))
            {
                TempData["StatusMessage"] = "Hostname Cannot be empty";
                TempData["StatusType"] = "danger";
                return Page();
            }
            // Validates if hostname already exists in inventory
            if (devices.Any(x => x.Hostname.ToLower() == Hostname.ToLower()))
            {
                TempData["StatusMessage"] = $"{Hostname} already exitst in the inventory";
                TempData["StatusType"] = "warning";
                return Page();
            }
            // Validates if site code is empty
            if (string.IsNullOrWhiteSpace(SiteCode))
            {
                TempData["StatusMessage"] = "Site must be selected";
                TempData["StatusType"] = "danger";
                return Page();
            }
            // Validates if Serial number is empty on physical devices
            if (isVirtualMachine == false && string.IsNullOrWhiteSpace(SerialNumber))
            {
                TempData["StatusMessage"] = "Serial Number cannot be empty for physical devices";
                TempData["StatusType"] = "danger";
                return Page();
            }

            _deviceService.AddDevice(new Devices
            {
                Hostname = Hostname,
                SiteCode = SiteCode,
                SerialNumber = SerialNumber,
                Model = Model,
                IP = IP,
                location = location,
                description = description,
                isVirtualMachine = isVirtualMachine,
                CPU = CPU,
                RAM = RAM,
                Storage = Storage,
                MACAddress = MACAddress,
                OS = OS

            });

            TempData["StatusMessage"] = $"{Hostname} added successfully";
            TempData["StatusType"] = "success";

            return RedirectToPage();

        }
    }
}

