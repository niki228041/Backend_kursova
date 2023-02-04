using _3dd_Data.Models;
using _3dd_Data.Models.Product_dir;
using _3dd_Data.Models.ViewModels;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegistrationViewModel, MyAppUser>();
            CreateMap<MyAppUser, RegistrationViewModel>();

            CreateMap<CompanyViewModel,Company>();
            CreateMap<Company,CompanyViewModel>();
            
            CreateMap<Product,ProductViewModel>();
            CreateMap< ProductViewModel, Product>();

            CreateMap<ProductComment, ProductMessageViewModel>();
            CreateMap<ProductMessageViewModel, ProductComment>();

        }
    }
}
