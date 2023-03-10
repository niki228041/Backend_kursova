using _3dd_Data.Db;
using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public class ProductCommentRepository : GenericRepository<ProductComment>, IProductCommentRepository
    {
        public ProductCommentRepository(DbContext3d context) : base(context)
        {
        }
        public IQueryable<ProductComment> Products => GetAll();

        public async Task DeleteByProductId(int id)
        {
            
            foreach (var com in Products)
            {
                if (com.ProductId == id)
                {
                    await Delete(com.Id);
                }
            }

        }
    }
}
