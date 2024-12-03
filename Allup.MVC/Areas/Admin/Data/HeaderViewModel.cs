using Allup.Application.ViewModels;
using Allup.Domain.Entities;

namespace Allup.MVC.Areas.Admin.Data
{
    public class HeaderViewModel
    {
        public List<LanguageViewModel>? Languages { get; set; }
        public LanguageViewModel? SelectedLanguage { get; set; }
    }
}
