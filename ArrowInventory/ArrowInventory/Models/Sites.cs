using System.ComponentModel.DataAnnotations;

namespace ArrowInventory.Models
{
    public class Sites
    {
        [Key]
        public string SiteCode { get; set; } = "";
        public string SiteName { get; set; } = "";
        public string siteCountry { get; set; } = "";

    }
}
