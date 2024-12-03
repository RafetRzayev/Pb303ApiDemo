using Microsoft.AspNetCore.Mvc.Rendering;

namespace Allup.Application.ViewModels;

public class ProductTranslationViewModel
{
    public int Id { get; set; }
    public string? Name {  get; set; }
    public string? Description { get; set; }
    public int ProductId { get; set; }
    public ProductViewModel? Product { get; set; }
    public int LanguageId { get; set; }
    public LanguageViewModel? LanguageViewModel {  get; set; }
}

public class ProductTranslationCreateViewModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProductId { get; set; }
    public ProductViewModel? Product { get; set; }
    public int LanguageId { get; set; }
    public List<SelectListItem>? Languages { get; set; }
    public string? Message { get; set; }
}