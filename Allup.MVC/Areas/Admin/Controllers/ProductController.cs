using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allup.MVC.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;

        public ProductController(IProductService productService, ICookieService cookieService)
        {
            _productService = productService;
            _cookieService = cookieService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var languageId = (await _cookieService.GetLanguageAsync()).Id;
            var product = await _productService.GetAsync(x => x.Id == id,
                include: x => x
                .Include(p => p.Category!)
                .ThenInclude(ct => ct.CategoryTranslations!.Where(pt => pt.LanguageId == languageId))
                .Include(p => p.ProductTranslations!)
                .ThenInclude(pt => pt.Language!));

            return View(product);
        }


        public async Task<IActionResult> Create()
        {
            var createViewModel = await _productService.GetProductCreateViewModelAsync();

            return View(createViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                createViewModel = await _productService.GetProductCreateViewModelAsync();

                return View(createViewModel);
            }

            var createdProduct = await _productService.CreateAsync(createViewModel);

            return RedirectToAction("Create", "ProductTranslation", new { productId = createdProduct.Id });
        }
    }
}
