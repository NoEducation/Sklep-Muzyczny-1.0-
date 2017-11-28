using SklepMuzyczny.DAT;
using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepMuzyczny.Infrastructure
{
    public interface ISongRepository
    {
        IEnumerable<Song> Songs { get; }
        IEnumerable<Category> Categories { get; }

        SongContext Context { get; }

        IEnumerable<Category> CategoriesWithSongsIncluded { get; }
        IEnumerable<Order> OrdersWithSongOrderedAndSongIncluded { get; }

        void AddOrder(Order orderToAdd);
    }
}
