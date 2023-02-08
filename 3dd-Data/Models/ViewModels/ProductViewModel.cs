
namespace _3dd_Data.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Version { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public DateTime UploadDate { get; set; }
        public string InWhichPrograms { get; set; }
        public string Extension { get; set; }
        public string LicenseType { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Data { get; set; }
        public IList<ProductImageUploadViewModel> Images_ { get; set; }
    }
}
