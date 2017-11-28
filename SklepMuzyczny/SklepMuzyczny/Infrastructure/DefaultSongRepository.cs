using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SklepMuzyczny.Models;
using SklepMuzyczny.DAT;

namespace SklepMuzyczny.Infrastructure
{
    public class DefaultSongRepository : ISongRepository
    {
        private SongContext context = new SongContext();

        public IEnumerable<Song> Songs { get { return context.Songs; } }
        public IEnumerable<Category> Categories { get { return context.Categories; } }

        public SongContext Context { get { return context; } } // poznej to usunę

        public IEnumerable<Category> CategoriesWithSongsIncluded{
            get{return context.Categories.Include("Song");}
        }
        public IEnumerable<Order> OrdersWithSongOrderedAndSongIncluded {
            get{return context.Orders.Include("SongOrdered").Include("SongOrdered.Song"); }
        }
        public void AddOrder(Order orderToAdd)
        {
            context.Orders.Add(orderToAdd);
            context.SaveChanges();
        }
        
    }
}