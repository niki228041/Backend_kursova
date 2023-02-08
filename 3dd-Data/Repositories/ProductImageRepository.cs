using _3dd_Data.Db;
using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(DbContext3d context) : base(context)
        {
        }

        public IQueryable<ProductImage> Products => GetAll();

        public async Task DeleteByProductId(int id)
        {
            foreach (var img in Products)
            {
                if (img.ProductId == id)
                {
                    await Delete(img.Id);
                }
            }
        }

        public string ReadImage(string imgName)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images", imgName);

                var bytesOfImage = File.ReadAllBytes(dir);

                var stringBytes = Convert.ToBase64String(bytesOfImage);

                return stringBytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
