using Microsoft.AspNetCore.Identity;

namespace ArrowInventory.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? DisplayName { get; set; }
        public DateTime? PasswordChangeDate { get; set; }


    }
}
