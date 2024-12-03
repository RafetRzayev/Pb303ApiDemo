using Microsoft.AspNetCore.Http;

namespace Allup.Application.Services.Abstracts
{
    public interface ICloudinaryService
    {
        Task<string> ImageCreateAsync(IFormFile file);
        Task<List<string>> ImageCreateAsync(List<IFormFile> file);
    }
}
