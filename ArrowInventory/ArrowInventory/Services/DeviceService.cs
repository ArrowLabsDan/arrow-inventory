using System.Text.Json;
using ArrowInventory.Models;


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

    }
}
