using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Areas.Admin.Controllers
{
    public class ProductImagesController : AdminController
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int productId)
        {
            var images = await _productImageService.GetAllAsync(x => x.ProductId == productId);
            ViewBag.ProductId = productId;
            return View(images);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productImage = await _productImageService.GetAsync(id);

            if (productImage == null) return NotFound();

            await _productImageService.Remove(id);

            return RedirectToAction("Details", new {productId = productImage.ProductId});
        }

        public async Task<IActionResult> AddImage(IFormFile newImage, int productId)
        {
            var createModel = new ProductImageCreateViewModel
            {
                ProductId = productId,
                ImageFile = newImage
            };

            await _productImageService.CreateAsync(createModel);
            return RedirectToAction("Details", new { productId });
        }

    }
}
