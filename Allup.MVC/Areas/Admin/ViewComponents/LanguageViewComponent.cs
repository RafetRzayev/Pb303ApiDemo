using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.MVC.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Areas.Admin.ViewComponents
{
    public class LanguageViewComponent : ViewComponent
    {
        private readonly ILanguageService _languageService;
        private readonly ICookieService _cookieService;

        public LanguageViewComponent(ILanguageService languageService, ICookieService cookieService)
        {
            _languageService = languageService;
            _cookieService = cookieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageService.GetAllAsync();
            var selectedLanguage = await _cookieService.GetLanguageAsync();
            var headerViewModel = new HeaderViewModel
            {
                Languages = languages,
                SelectedLanguage = selectedLanguage
            };

            return View(headerViewModel);
        }
    }
}
