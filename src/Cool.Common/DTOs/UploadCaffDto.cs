using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cool.Common.DTOs
{
    public class UploadCaffDto
    {
        [FromForm]
        public IFormFile File { get; set; }
        public List<string> Tags { get; set; }
    }
}
