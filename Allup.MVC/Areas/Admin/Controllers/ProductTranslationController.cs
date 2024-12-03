using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Areas.Admin.Controllers
{
    public class ProductTranslationController : AdminController
    {
        private readonly IProductTranslationService _productTranslationService;
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductTranslationController(IProductService productService, IProductTranslationService productTranslationService, ILanguageService languageService)
        {
            _productService = productService;
            _productTranslationService = productTranslationService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int productId)
        {
            var createModel = await _productTranslationService.GetProductTranslationCreateViewModelAsync(productId);

            return View(createModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTranslationCreateViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                createModel = await _productTranslationService.GetProductTranslationCreateViewModelAsync(createModel.ProductId);

                return View(createModel);
            }

            var createdProductTranslation = await _productTranslationService.CreateAsync(createModel);

            createModel = await _productTranslationService.GetProductTranslationCreateViewModelAsync(createModel.ProductId);
            createModel.Message = "Elave olundu";
            return View(createModel);
        }
    }
}
