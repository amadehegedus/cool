using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Dal.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int CaffId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Caff Caff { get; set; }
    }
}
