using ArrowInventory.Models;
using System.Text.Json;

namespace ArrowInventory.Services
{
    public class SiteService
    {

        private readonly string _filepath = "Data/sites.json";

        public List<Sites> GetSites()
        {
            var json = File.ReadAllText(_filepath);
            return JsonSerializer.Deserialize<List<Sites>> (json)
                ?? new List<Sites>();
        }
        public void SaveSites(List<Sites> sites)
        {
            var json = JsonSerializer.Serialize(sites, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filepath, json);
        }

        public void DeleteSite(string siteCode)
        {
            var sites = GetSites();
            var siteToRemove = sites.FirstOrDefault(x => x.SiteCode.ToLower() == siteCode.ToLower());

            if (siteToRemove != null)
            {
                sites.Remove(siteToRemove);
                SaveSites(sites);
            }
        }

    }
}
