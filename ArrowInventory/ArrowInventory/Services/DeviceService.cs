using ArrowInventory.Models;
using ArrowInventory.Data;



namespace ArrowInventory.Services
{
    public class DeviceService
    {

        private readonly AppDbContext _context;

        public DeviceService(AppDbContext context)
        {
            _context = context;
        }
        public List<Devices> GetDevices()
        {
            return _context.Devices.ToList();
        }
        public void SaveDevices(List<Devices> Devices)
        {
        // To be removed once AddDevices is amended
        }

        // Add Devices to table
        public void AddDevice(Devices device)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
        }

        // Update existing devices in table
        public void UpdateDevices(Devices updated)
        {
            var existing = _context.Devices.Find(updated.Hostname);
            if (existing != null)
            {
                existing.SerialNumber = updated.SerialNumber;
                existing.SiteCode = updated.SiteCode;
                existing.Model = updated.Model;
                existing.isVirtualMachine = updated.isVirtualMachine;
                existing.IP = updated.IP;
                existing.description = updated.description;
                existing.location = updated.location;
                existing.CPU = updated.CPU;
                existing.RAM = updated.RAM;
                existing.Storage = updated.Storage;
                existing.MACAddress = updated.MACAddress;
                existing.OS = updated.OS;
                _context.SaveChanges();
            }
        }

        // Deletre devices from table
        public void DeleteDevice(string hostname)
        {
            var device = _context.Devices.Find(hostname);
            if(device != null)
            {
                _context.Devices.Remove(device);
                _context.SaveChanges();

            }
        }

    }
}
