using ArrowInventory.Models;
using System.Text.Json;


namespace ArrowInventory.Services
{
    public class DeviceService
    {
        private readonly string _filePath = "Data/devices.json";
        public List<Devices> GetDevices()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Devices>>(json)
                ?? new List<Devices>();
        }
        public void SaveDevices(List<Devices> Devices)
        {
            var json = JsonSerializer.Serialize(Devices, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }

        public void DeleteDevice(string hostname)
        {
            var devices = GetDevices();

            var deviceToRemove = devices.FirstOrDefault(x => x.Hostname == hostname);

            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
                SaveDevices(devices);
            }
        }

        public void UpdateDevices(Devices updated)
        {
            var devices = GetDevices();
            var existing = devices.FirstOrDefault(x => x.Hostname == updated.Hostname);

            if(existing != null)
            {
                existing.SerialNumber = updated.SerialNumber;
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
                SaveDevices(devices);
            }
        }



    }
}
