using System;
using System.Collections.Generic;
using System.Text;

namespace Cool.Dal.Entities
{
    public class Caff
    {
        public Caff()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string Creator { get; set; }
        public DateTime CreationTime { get; set; }
        public string FilePath { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
