using _3dd_Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3dd_Data.Models.Product_dir
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual List<ProductImage> Images { get; set; }
        public string Size { get; set; }
        public int Likes { get; set; }
        public int Stars { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual string InWhichPrograms { get; set; }
        public string Extension { get; set; }
        public string LicenseType { get; set; }
        public string AssetName { get; set; }
        public string Version { get; set; }

        public virtual ICollection<ProductComment> ProductComments { get; set; }


        //Every product have a creator
        public MyAppUser User { get; set; }

        [ForeignKey(nameof(MyAppUser))]
        public int? UserId { get; set; }


        //Every product have a company
        public Company Company_ { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
    }
}
