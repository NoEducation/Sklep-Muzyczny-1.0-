using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SklepMuzyczny.Models;

namespace SklepMuzyczny.ViewModels
{
    public class SongsByCategorySongViewModel
    {
        public IPagedList<Song> Songs { get; set; }
        public string ChoosenCategory { get; set; } 
    }
}