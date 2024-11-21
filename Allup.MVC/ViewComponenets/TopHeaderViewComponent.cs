﻿using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Allup.MVC.ViewComponenets
{
    public class TopHeaderViewComponent : ViewComponent
    {
        private readonly ILanguageService _languageService;

        public TopHeaderViewComponent(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var languages = await _languageService.GetLanguagesAsync();
            var culture = Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            var isoCode = culture?.Substring(culture.LastIndexOf("=") + 1) ?? "en-Us";
            var selectedLanguage = await _languageService.GetLanguageAsync(isoCode);

            var topHeaderViewModel = new TopHeaderViewModel
            {
                Languages = languages,
                SelectedLanguage = selectedLanguage
            };

            return View(topHeaderViewModel);
        }
    }
}
