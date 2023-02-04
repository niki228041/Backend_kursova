using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime DateCreated { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
