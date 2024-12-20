﻿using Allup.Application.Services.Abstracts;
using Allup.Domain.AppSettingsModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Allup.Application.Services.Implementations
{
    public class CloudinaryManager : ICloudinaryService
    {
        private readonly IConfiguration _configuration;

        public CloudinaryManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ImageCreateAsync(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
           
            var cloudinarySettings = _configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            var myAccount = new Account
            {
                ApiKey = cloudinarySettings?.Key,
                ApiSecret = cloudinarySettings?.Secret,
                Cloud = cloudinarySettings?.CloudName
            };

            Cloudinary _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            string url = uploadResult.SecureUrl.ToString();

            return url;
        }

        public Task<List<string>> ImageCreateAsync(List<IFormFile> file)
        {
            throw new NotImplementedException();
        }
    }
}
