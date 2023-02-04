using _3dd_Data.Models.Product_dir;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Models.ViewModels
{
    public class ProductMessageViewModel
    {
        public string Message { get; set; }
        public int Stars { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
