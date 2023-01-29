using Microsoft.AspNetCore.Identity;

namespace _3dd_Data.Models
{
    public class MyAppRoleUser : IdentityUserRole<int>
    {
        public virtual MyAppUser User { get; set; }
        public virtual MyAppRole Role { get; set; }
    }
}
