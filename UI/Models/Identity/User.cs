using Microsoft.AspNetCore.Identity;

namespace UI.Models.Identity
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? Bithday { get; set; }
        public string? Sex { get; set; }
    }
}
