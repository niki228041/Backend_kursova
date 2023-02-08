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
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ICompanyRepository companyRepository,IMapper mapper, IProductCommentRepository productCommentRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _productCommentRepository = productCommentRepository;
            _productImageRepository = productImageRepository;
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("UploadImage")]
        public async Task<string> UploadImage(ProductImageUploadViewModel model)
        {
            string fileName = string.Empty;

            try
            {
                if (model.Data != null)
                {
                    var fileExp = model.Extension;
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    fileName = string.Format(@"{0}" + fileExp, Guid.NewGuid());


                    byte[] byteBuffer = Convert.FromBase64String(model.Data);
                    System.IO.File.WriteAllBytes(Path.Combine(dir, fileName), byteBuffer);
                }
            }catch (Exception ex)
            {

            }

            return fileName;

            //string port = string.Empty;
            //if(Request.Host.Port!=null)
            //{
            //    port = ":"+Request.Host.Port.ToString();
            //}
            //var url = $@"{Request.Scheme}://{Request.Host.Host}{port}/images/{fileName}";

            //return Ok(url);
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

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            Product new_product = _mapper.Map<Product>(model);

            var fileName = await UploadAsset(new ProductAssetViewModel {Data = model.Data, Extension = model.Extension });

            new_product.AssetName = fileName;
            new_product.Extension = model.Extension;
            
            


            await _productRepository.Create(new_product);

            var my_product = _productRepository.GetAll().FirstOrDefault(prod=>prod==new_product);

            //set first uploaded img as first
            bool isFirstPicture = true;

            foreach (var img in model.Images_)
            {
                var imgTemplate = new ProductImageUploadViewModel { Data = img.Data,Extension = img.Extension };
                var imgFileName = await UploadImage(imgTemplate);
                ProductImage new_img_to_upload = new ProductImage { Name = imgFileName, ProductId = my_product.Id };

                if (isFirstPicture == true)
                {
                    new_img_to_upload.isFirstPhoto = true;
                    isFirstPicture = false;
                }

                await _productImageRepository.Create(new_img_to_upload);
            }

            return Ok(my_product);
        }

        [HttpPost]
        [Route("AllProduct")]
        public async Task<IActionResult> AllProduct(FindBy findBy)
        {
            if (!string.IsNullOrEmpty(findBy.findBy))
            {
                var products = _productRepository.GetAll().Where(prod => prod.Name.Contains(findBy.findBy));
                return Ok(products);
            }
            else
            {
                var products = _productRepository.GetAll();
                return Ok(products);
            }
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

        [HttpPost]
        [Route("AllCommentByProductId")]
        public async Task<IActionResult> AllCommentByProductId(IdViewModel model)
        {
            var comments = _productCommentRepository.GetAll().Where(com=>com.ProductId == model.Id);

            return Ok(comments);
        }

        
        [HttpPost]
        [Route("GetImagesByProductId")]
        public async Task<IActionResult> GetImagesByProductId(IdViewModel model)
        {
            var product = _productRepository.GetAll().First(prod=>prod.Id == model.Id);

            var images_to_fontend = new List<ProductImageToSendViewModel>();
            
            var images = _productImageRepository.GetAll().Where(img => img.ProductId == product.Id);

            foreach(var image in images)
            {
                var img_in_bytes = _productImageRepository.ReadImage(image.Name);
                images_to_fontend.Add(new ProductImageToSendViewModel { Data = img_in_bytes, isFirst = image.isFirstPhoto });
                
            }
            
            IQueryable images_to_fontend_query = images_to_fontend.AsQueryable();


            return Ok(images_to_fontend_query); 
        }

        [HttpPost]
        [Route("GetFirstImages")]
        public async Task<IActionResult> GetFirstImages()
        {

            var images_to_fontend = new List<ProductImageToSendViewModel>();

            var images = _productImageRepository.GetAll().Where(img=>img.isFirstPhoto==true);

            foreach (var image in images)
            {
                var img_in_bytes = _productImageRepository.ReadImage(image.Name);
                images_to_fontend.Add(new ProductImageToSendViewModel { Data = img_in_bytes, isFirst = image.isFirstPhoto, productId=image.ProductId });

            }

            //IQueryable images_to_fontend_query = images_to_fontend.AsQueryable();


            return Ok(images_to_fontend);
        }

        [HttpPost]
        [Route("DeleteAllImagesById")]
        public async Task DeleteAllImagesById(IdViewModel model)
        {
            await _productImageRepository.DeleteByProductId(model.Id);
        }


        [HttpPost]
        [Route("DeleteAllCommentsById")]
        public async Task DeleteAllCommentsById(IdViewModel model)
        {
            await _productCommentRepository.DeleteByProductId(model.Id);
        }

        [HttpPost]
        [Route("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(IdViewModel model)
        {
            await DeleteAllImagesById(model);
            await DeleteAllCommentsById(model);
            await _productRepository.Delete(model.Id);
            return Ok();
        }

        [HttpPost]
        [Route("GetAssetByProductId")]
        public async Task<IActionResult> GetAssetByProductId(IdViewModel model)
        {
            var assetBytes = _productRepository.GetAssetByProductId(model.Id);

            return Ok(assetBytes);
        }



    }
}
