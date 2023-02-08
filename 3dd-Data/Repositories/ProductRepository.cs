using _3dd_Data.Db;
using _3dd_Data.Models.Product_dir;
using _3dd_Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _3dd_Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(DbContext3d context) : base(context)
        {
        }

        public IQueryable<Product> Products => GetAll();


        public ProductAssetViewModel ReadAsset(string assetName)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Assets", assetName);

                var bytesOfAsset = File.ReadAllBytes(dir);
                var ext = Path.GetExtension(dir);

                var stringBytes = Convert.ToBase64String(bytesOfAsset);

                return new ProductAssetViewModel { Data = stringBytes, Extension = ext };
            }
            catch
            {
                return null;
            }
        }

        public ProductAssetViewModel GetAssetByProductId(int id)
        {
            var product = GetAll().First(prod => prod.Id == id);

            var asset_in_bytes = ReadAsset(product.AssetName);

            return asset_in_bytes;
        }

    }
}
