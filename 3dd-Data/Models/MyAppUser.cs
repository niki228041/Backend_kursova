using _3dd_Data.Models.Product_dir;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3dd_Data.Models
{
    public class MyAppUser : IdentityUser<int>
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public virtual ICollection<MyAppRoleUser> UserRoles { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        //public virtual ICollection<ProductComment> Messages { get; set; }
    }
}
