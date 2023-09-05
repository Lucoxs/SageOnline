using Microsoft.AspNetCore.Identity;

namespace API.Identity.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
