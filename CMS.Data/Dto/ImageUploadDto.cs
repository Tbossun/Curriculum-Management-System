using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Dto
{
    public class ImageUploadDto
    {
        public IFormFile file { get; set; }
    }
}
