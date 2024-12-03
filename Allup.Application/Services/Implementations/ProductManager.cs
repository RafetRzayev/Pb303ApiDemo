using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.Services.Implementations;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Context;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace Allup.Application.Services.Implementations;

public class ProductManager :  CrudManager<ProductViewModel, Product, ProductCreateViewModel>, IProductService
{
    private readonly EfRepositoryBase<Product, AppDbContext> _repository;
    private readonly ICategoryService _categoryService;
    private readonly ExternalApiService _externalApiService;
    private readonly ICookieService _cookieService;
    private readonly ICloudinaryService _cloudinaryService;

    public ProductManager(EfRepositoryBase<Product, AppDbContext> repository, IMapper mapper, ExternalApiService externalApiService, ICookieService cookieService, ICategoryService categoryService, ICloudinaryService cloudinaryService) : base(repository, mapper)
    {
        _repository = repository;
        _externalApiService = externalApiService;
        _cookieService = cookieService;
        _categoryService = categoryService;
        _cloudinaryService = cloudinaryService;
    }

    public override async Task<List<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null,
                                    Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
                                    Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var products = await _repository.GetAllAsync(predicate, orderBy, include);

        var productViewModels = Mapper.Map<List<ProductViewModel>>(products);

        var currency = await _cookieService.GetCurrencyAsync();

        var coefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
        var culture = new CultureInfo(currency.IsoCode?? "az-az");

        foreach (var item in productViewModels)
        {
            item.FormattedPrice = (item.Price / coefficient).ToString("C", culture);
        }

        return productViewModels;
    }

    public override async Task<ProductViewModel> GetAsync(Expression<Func<Product, bool>> predicate, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null)
    {
        var product = await _repository.GetAsync(predicate, include);
        
        var productViewModel = Mapper.Map<ProductViewModel>(product);

        var currency = await _cookieService.GetCurrencyAsync();

        var coefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
        var culture = new CultureInfo(currency.IsoCode?? "az-az");

        productViewModel.FormattedPrice = (productViewModel.Price / coefficient).ToString("C", culture);

        return productViewModel;
    }

    public async Task<ProductCreateViewModel> GetProductCreateViewModelAsync()
    {
        var productCreateViewModel = new ProductCreateViewModel();
        var categorySelectListItems = new List<SelectListItem>();
        var language = await _cookieService.GetLanguageAsync();
        var categories = await _categoryService.GetAllAsync(include:
            x => x
            .Include(c => c.CategoryTranslations!.Where(ct => ct.LanguageId == language.Id)));

        categories.ForEach(c => categorySelectListItems.Add(new SelectListItem(c.Name, c.Id.ToString())));

        productCreateViewModel.CategoryList = categorySelectListItems;

        return productCreateViewModel;
    }

    public override async Task<ProductViewModel> CreateAsync(ProductCreateViewModel createViewModel)
    {
        createViewModel.CoverImageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.CoverImageFile);
        createViewModel.HoverImageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.HoverImageFile);

        var createdProductEntity = Mapper.Map<Product>(createViewModel);
        createdProductEntity.ProductImages = new List<ProductImage>();

        foreach (var item in createViewModel.Images ?? [])
        {
            var imageUrl = await _cloudinaryService.ImageCreateAsync(item);

            createdProductEntity.ProductImages.Add(
                new ProductImage
                { 
                    ImageUrl = imageUrl,
                    ProductId = createdProductEntity.Id 
                });
        }

        createdProductEntity = await _repository.AddAsync(createdProductEntity);

        return Mapper.Map<ProductViewModel>(createdProductEntity);
    }
}
