using Microsoft.AspNetCore.Identity;

namespace DevLinker.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
