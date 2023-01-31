using _3dd_Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3dd_Api_kusova.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ProductImageUploadViewModel model)
        {
            string fileName = string.Empty;
            
            if(model.Image != null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName);
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExp;
                using (var stream = System.IO.File.Create(Path.Combine(dir,fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }

            }
            string port = string.Empty;
            if(Request.Host.Port!=null)
            {
                port = ":"+Request.Host.Port.ToString();
            }
            var url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/{fileName}";

            return Ok(url);
        }
    }
}
