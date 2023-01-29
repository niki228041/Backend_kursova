using Microsoft.AspNetCore.Identity;

namespace _3dd_Data.Models
{
    public class MyAppRole : IdentityRole<int>
    {
        public virtual ICollection<MyAppRoleUser> UserRoles { get; set; }

    }
}
