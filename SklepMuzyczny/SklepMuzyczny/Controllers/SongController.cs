using SklepMuzyczny.DAT;
using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.Models;
using SklepMuzyczny.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace SklepMuzyczny.Controllers
{
    public class SongController : Controller
    {
        ISongRepository repository;
        public SongController(ISongRepository repo)
        {
            repository = repo;
        }
        
        public ViewResult SongsByCategory(string categoryName = null, int? page = null)
        {
            const int PageItems = 5;
            int currentPage = (page ?? 1);

            SongsByCategorySongViewModel model = new SongsByCategorySongViewModel();

            if(categoryName == null || categoryName == SklepMuzyczny.Const.ConstValues.AllSongs) // wcześniej było kompletnie bez sensu !
            {
                model.Songs = repository.Songs
                    .OrderBy(x=> x.NameSong)
                    .ToPagedList(currentPage, PageItems);
            }
            else // z wybranej kategorij
            {
                var category = repository.CategoriesWithSongsIncluded
                    .Where(x => x.NameCategory == categoryName)
                    .Single();

                model.Songs = category.Song
                    .OrderBy(x=> x.NameSong)
                    .ToPagedList(currentPage, PageItems);
            }

            model.ChoosenCategory = categoryName;
            return View(model); 
        }
        public ViewResult SongDetails(int songId)
        {
           var song = repository.Songs.FirstOrDefault(x => x.SongId == songId);
           return View(song);
        }
        public PartialViewResult MenuVertical(string categoryName = null)
        {
            ViewBag.ChoosenCategory = categoryName;
            var categories = repository.Categories.ToList();
            return PartialView("_MenuVertical",categories);
        }

    }
}