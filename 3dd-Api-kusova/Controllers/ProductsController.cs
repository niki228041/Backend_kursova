using _3dd_Data.Models;
using _3dd_Data.Models.Product_dir;
using _3dd_Data.Models.ViewModels;
using _3dd_Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace _3dd_Api_kusova.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ICompanyRepository companyRepository,IMapper mapper, IProductCommentRepository productCommentRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
        }

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

        [HttpPost]
        [Route("UploadAsset")]
        public async Task<string> UploadAsset(ProductAssetViewModel model)
        {
            string fileName = string.Empty;

            if (model.Data != null)
            {
                var fileExp = model.Extension;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
                fileName = string.Format(@"{0}."+ fileExp, Guid.NewGuid());


                byte[] byteBuffer = Convert.FromBase64String(model.Data);

                System.IO.File.WriteAllBytes(Path.Combine(dir, fileName), byteBuffer);
                  

            }
            
            return fileName;
        }

        [HttpPost]
        [Route("CreateCompany")]
        public async Task<IActionResult> CreateCompany(CompanyViewModel model)
        {

            Company new_company = _mapper.Map<Company>(model);
            await _companyRepository.Create(new_company);

            return Ok(new_company);
        }


        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            Product new_product = _mapper.Map<Product>(model);

            var fileName = await UploadAsset(new ProductAssetViewModel {Data = model.Data, Extension = model.Extension });

            new_product.AssetName = fileName;
            new_product.Extension = model.Extension;


            await _productRepository.Create(new_product);
            return Ok(new_product);
        }

        [HttpGet]
        [Route("AllProduct")]
        public async Task<IActionResult> AllProduct()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("AllCompany")]
        public async Task<IActionResult> AllCompany()
        {
            var companies = _companyRepository.GetAll();
            return Ok(companies);
        }

        [HttpPost]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(IdViewModel model)
        {
            var product = await _productRepository.GetById(model.Id);
            return Ok(product);
        }

        [HttpPost]
        [Route("GetProductByUserId")]
        public async Task<IActionResult> GetProductByUserId(IdViewModel model)
        {
            List<Product> product_to_frontend = new List<Product>();

            var products = _productRepository.GetAll();
            foreach(var product in products)
            {
                if(product.UserId == model.Id)
                product_to_frontend.Add(product);
            }
            return Ok(product_to_frontend);
        }

        [HttpPost]
        [Route("CreateNewProductMessage")]
        public async Task<IActionResult> CreateNewProductMessage(ProductMessageViewModel model)
        {
            var newMessage = _mapper.Map<ProductComment>(model);
            await _productCommentRepository.Create(newMessage);
            
            return Ok(newMessage);
        }

    }
}
