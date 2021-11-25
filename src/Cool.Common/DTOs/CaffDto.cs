using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Common.DTOs
{
    public class CaffDto
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime CreationTime { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<CommentDto> Comments { get; set; }
        public byte[] PreviewBitmap { get; set; }
    }
}
