using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Common.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
