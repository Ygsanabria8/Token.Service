using Microsoft.AspNetCore.Identity;

namespace WaoVet.Token.Service.Domain
{
    public class UserModel : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
