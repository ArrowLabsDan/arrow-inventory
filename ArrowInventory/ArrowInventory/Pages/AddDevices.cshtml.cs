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
        public string IP { get; set; } = "0.0.0.0";
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

        public void OnPost()
        {

            Sites = _siteService.GetSites();
            var devices = _deviceService.GetDevices();

            // Validates if hostname is empty
            if (string.IsNullOrWhiteSpace(Hostname))
            {
                StatusMessage = "Hostname Cannot be empty";
                StatusType = "danger";
                return;
            }
            // Validates if hostname already exists in inventory
            if (devices.Any(x => x.Hostname.ToLower() == Hostname.ToLower()))
            {
                StatusMessage = $"{Hostname} already exitst in the inventory";
                StatusType = "warning";
                return;
            }
            // Validates if site code is empty
            if (string.IsNullOrWhiteSpace(SiteCode))
            {
                StatusMessage = "Site must be selected";
                StatusType = "danger";
                return;
            }
            // Validates if Serial number is empty on physical devices
            if (isVirtualMachine == false && string.IsNullOrWhiteSpace(SerialNumber))
            {
                StatusMessage = "Serial Number cannot be empty for physical devices";
                StatusType = "danger";
                return;
            }

            devices.Add(new Devices
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

            _deviceService.SaveDevices(devices);

            StatusMessage = $"{Hostname} added successfully";
            StatusType = "success";

            // Clear UI state + reset form
            ModelState.Clear();
            Hostname = "";
            SerialNumber = "";
            Model = "";
            isVirtualMachine = false;
            IP = "0.0.0.0";
            CPU = "";
            RAM = "";
            Storage = "";
            MACAddress = "";
            OS = "";
            SiteCode = "";

        }
    }
}

