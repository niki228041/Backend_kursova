using _3dd_Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3dd_Data.Models.Product_dir
{
    public class ProductImage : BaseEntity<int>
    {
        public string Name { get; set; }

        //Every product have a creator
        public Product Product { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
    }
}
