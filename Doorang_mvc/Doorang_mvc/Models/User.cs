using Microsoft.AspNetCore.Identity;

namespace Doorang_mvc.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
