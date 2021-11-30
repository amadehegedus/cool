using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Common.DTOs
{
    public class UploadCaffDto
    {
        public List<string> Tags { get; set; }
        public string CaffBase64String { get; set; }
    }
}
