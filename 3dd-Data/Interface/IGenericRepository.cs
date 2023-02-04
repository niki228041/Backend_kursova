using _3dd_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Interface
{
    public interface IGenericRepository<TEntity,T> where TEntity : class , IEntity<T>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(T id);
        Task Update(TEntity entity);
        Task Create(TEntity entity);
        Task Delete(T id);
    }
}
