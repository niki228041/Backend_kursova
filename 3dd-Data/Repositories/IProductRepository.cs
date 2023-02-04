using _3dd_Data.Interface;
using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public interface IProductRepository : IGenericRepository<Product,int>
    {
        IQueryable<Product> Products { get; }
    }
}
