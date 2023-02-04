using _3dd_Data.Db;
using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(DbContext3d context) : base(context)
        {
        }

        public IQueryable<Product> Products => GetAll();
    }
}
