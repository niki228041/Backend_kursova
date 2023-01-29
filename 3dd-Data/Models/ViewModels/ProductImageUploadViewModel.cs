using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dd_Data.Models.ViewModels
{
    public class ProductImageUploadViewModel
    {
        public IFormFile Image { get; set; }

    }
}
