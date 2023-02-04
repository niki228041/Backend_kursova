using _3dd_Data.Entities;

namespace _3dd_Data.Models.Product_dir
{
    public class Company : BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime CreatedIn { get; set; }
        public string License { get; set; }
        public virtual ICollection<Product> ProductComments { get; set; }
    }
}
