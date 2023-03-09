using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime date { get; set; }
    }
}
