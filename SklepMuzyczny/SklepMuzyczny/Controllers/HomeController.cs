using SklepMuzyczny.DAT;
using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepMuzyczny.Controllers
{
    public class HomeController : Controller
    {
        private ISongRepository repository;
        public HomeController(ISongRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            IndexHomeViewModel model = new IndexHomeViewModel();
            
            model.Bestsellers = repository.Songs.Where(x => x.Bestseller)
                .Take(5)
                .ToList();
            model.Newest = repository.Songs.OrderBy(x => x.DateAdded)
                .Take(5)
                .ToList();
               
             return View(model);
        }
        public PartialViewResult MenuHorizontal()
        {
           var category = repository.Categories.ToList();
            return PartialView("_MenuHorizontal", category);
        }
        public ViewResult StaticSites(string id)
        {
            return View(id);
        }
    }
}