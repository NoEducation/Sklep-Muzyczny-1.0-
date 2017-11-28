using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }
        public string Decription { get; set; }
        public string NameFilePicture { get; set; }

        public virtual ICollection<Song> Song { get; set; }
    }
}