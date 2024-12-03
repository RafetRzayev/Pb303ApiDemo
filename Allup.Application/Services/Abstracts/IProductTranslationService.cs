using Allup.Application.ViewModels;
using Allup.Domain.Entities;

namespace Allup.Application.Services.Abstracts;

public interface IProductTranslationService : ICrudService<ProductTranslationViewModel, ProductTranslation, ProductTranslationCreateViewModel>
{
    Task<ProductTranslationCreateViewModel> GetProductTranslationCreateViewModelAsync(int productId);
}