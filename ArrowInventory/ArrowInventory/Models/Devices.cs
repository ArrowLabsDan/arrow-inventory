namespace ArrowInventory.Models
{
    public class Devices
    {
        public string Hostname { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string Model { get; set; } = "";
        public bool isVirtualMachine { get; set; } = false;
        public string IP { get; set; } = "0.0.0.0";
        public string description { get; set; } = "";
        public string location { get; set; } = "";
    }
}
