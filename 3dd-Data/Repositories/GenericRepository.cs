using _3dd_Data.Db;
using _3dd_Data.Entities;
using _3dd_Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        private readonly DbContext3d _dbContext;

        public GenericRepository(DbContext3d dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using(var db_ = new DbContext3d()) { 
                var entity = await GetById(id);
                db_.Set<TEntity>().Remove(entity);
                await db_.SaveChangesAsync();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            using (var db_ = new DbContext3d())
            {
                return await db_.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            }

            
        } 

        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
