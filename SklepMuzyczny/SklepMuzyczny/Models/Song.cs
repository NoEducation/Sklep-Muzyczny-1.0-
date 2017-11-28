using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public int CategoryId { get; set; }
        public string NameSong { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Bestseller { get; set; }
        public string NameFilePicture { get; set; }

        public virtual Category Category { get; set; }
    }
}