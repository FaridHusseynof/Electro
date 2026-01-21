using Microsoft.AspNetCore.Identity;

namespace Electro.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsActivated { get; set; }
    }
}
