using _3dd_Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3dd_Data.Models.Product_dir
{
    public class ProductComment : BaseEntity<int>
    {
        public string Message { get; set; }
        public int Stars { get; set; }

        //Every comment have a creator
        public MyAppUser User { get; set; }

        [ForeignKey(nameof(MyAppUser))]
        public int? UserId { get; set; }


        //Every comment have a product
        public Product Product_ { get; set; }

        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
    }
}
