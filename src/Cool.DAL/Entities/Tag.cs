using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Dal.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CaffId { get; set; }
        public virtual Caff Caff { get; set; }
    }
}
