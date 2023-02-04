using _3dd_Data.Db;
using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext3d dbContext) : base(dbContext)
        {
        }

    }
}
