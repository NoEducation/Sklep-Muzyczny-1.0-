using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.ViewModels
{
    public class IndexHomeViewModel
    {
        public IList<Song> Bestsellers { get; set; }
        public IList<Song> Newest { get; set; }
    }
}