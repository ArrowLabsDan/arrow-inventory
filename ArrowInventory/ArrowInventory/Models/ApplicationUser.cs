using Microsoft.AspNetCore.Identity;

namespace ArrowInventory.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? DisplayName { get; set; }

    }
}
