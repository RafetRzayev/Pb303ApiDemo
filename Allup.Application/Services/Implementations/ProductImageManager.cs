using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Context;
using AutoMapper;
using Core.Persistence.Repositories;

namespace Allup.Application.Services.Implementations
{
    public class ProductImageManager : CrudManager<ProductImageViewModel, ProductImage, ProductImageCreateViewModel>, IProductImageService
    {
        private readonly EfRepositoryBase<ProductImage, AppDbContext> _repository;
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductImageManager(EfRepositoryBase<ProductImage, AppDbContext> repository, IMapper mapper, IProductService productService, ILanguageService languageService, ICloudinaryService cloudinaryService) : base(repository, mapper)
        {
            _repository = repository;
            _productService = productService;
            _languageService = languageService;
            _cloudinaryService = cloudinaryService;
        }

        public override async Task<ProductImageViewModel> CreateAsync(ProductImageCreateViewModel createViewModel)
        {
            var imageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.ImageFile);
            createViewModel.ImageUrl = imageUrl;

            return  await base.CreateAsync(createViewModel);
        }

    }
}
