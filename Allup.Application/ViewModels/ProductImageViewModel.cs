using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.ViewModels
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId {  get; set; }
    }

    public class ProductImageCreateViewModel
    {
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
    }
}
