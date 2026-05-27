using System.ComponentModel.DataAnnotations;

namespace ArrowInventory.Models
{
    public class Devices
    {
        [Key]
        public string Hostname { get; set; } = "";
        public string SiteCode { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public bool isVirtualMachine { get; set; } = false;

        public string? Model { get; set; } = "";
        public string? IP { get; set; } = "";
        public string? description { get; set; } = "";
        public string? location { get; set; } = "";
        public string? CPU { get; set; } = "";
        public string? RAM { get; set; } = "";
        public string? Storage { get; set; } = "";
        public string? MACAddress { get; set; } = "";
        public string? OS { get; set; } = "";

    }
}
