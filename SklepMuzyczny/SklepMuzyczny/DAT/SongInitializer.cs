using SklepMuzyczny.Migrations;
using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.DAT
{
    public class SongInitializer : MigrateDatabaseToLatestVersion<SongContext,Configuration>
    {
        public static void SetDefaultValue(SongContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category(){CategoryId=1,Decription="Opis",NameCategory="Pop",NameFilePicture="Testowa wartosc"},
                new Category(){CategoryId=2,Decription="Opis",NameCategory="Rap",NameFilePicture="Testowa wartosc"},
                new Category(){CategoryId=3,Decription="Opis",NameCategory="Jazz",NameFilePicture="Testowa wartosc"},
                new Category(){CategoryId=4,Decription="Opis",NameCategory="Muzyka Klasyczna",NameFilePicture="Testowa wartosc"},
                new Category(){CategoryId=5,Decription="Opis",NameCategory="Rock",NameFilePicture="Testowa wartosc"},
            };
            categories.ForEach(x => context.Categories.AddOrUpdate(x));
            context.SaveChanges();
            List<Song> songs = new List<Song>()
            {
                new Song(){SongId=1,NameSong="Diamonds", Author="Rihanna",DateAdded= DateTime.Parse("05/11/2017 15:21:35"),
                    Description ="Opis",Price=30,NameFilePicture="diamonds.jpg",Bestseller=true,CategoryId=1},
                new Song(){SongId=2,NameSong="Umbrella", Author="Rihanna",DateAdded= DateTime.Parse("04/11/2017 15:22:35"),
                    Description ="Opis",Price=30,NameFilePicture="umbrella.jpg",Bestseller=true,CategoryId=1},
                new Song(){SongId=3,NameSong="In the end", Author="Linkin Park",DateAdded= DateTime.Parse("02/11/2017 13:21:21"),
                    Description ="Opis",Price=60,NameFilePicture="intheend.jpg",Bestseller=false,CategoryId=5},
                new Song(){SongId=4,NameSong="Half Dead", Author="Quebonafide ft. ReTo ",DateAdded= DateTime.Parse("05/11/2017 15:21:35"),
                    Description ="Opis",Price=20,NameFilePicture="halfdead.jpg",Bestseller=true,CategoryId=2},
                new Song(){SongId=5,NameSong="Marsz pogrzebowy", Author="Chopin",DateAdded= DateTime.Parse("05/10/2017 15:21:35"),
                    Description ="Opis",Price=100,NameFilePicture="marszpogrzebowy.jpg",Bestseller=true,CategoryId=4},
                new Song(){SongId=6,NameSong="Shape of You", Author="Ed Sheeran",DateAdded= DateTime.Parse("05/11/2016 15:21:35"),
                    Description ="Opis",Price=20,NameFilePicture="shapeofyou.jpg",Bestseller=false,CategoryId=1},
                new Song(){SongId=7,NameSong="Sex in Fire", Author="Kings of Leon",DateAdded= DateTime.Parse("05/11/2013 15:21:35"),
                    Description ="Opis",Price=30,NameFilePicture="sexInTheFire.jpg",Bestseller=true,CategoryId=3},
                new Song(){SongId=8,NameSong="Hey you", Author="Pink Floyd",DateAdded= DateTime.Parse("05/10/2017 15:21:35"),
                    Description ="Opis",Price=34,NameFilePicture="heyyou.jpg",Bestseller=false,CategoryId=1},
                new Song(){SongId=9,NameSong="Requiem", Author="Wolfgang Amadeus Mozart",DateAdded= DateTime.Parse("04/11/2017 15:21:35"),
                    Description ="Opis",Price=54,NameFilePicture="Requiem.jpg",Bestseller=true,CategoryId=4},
                new Song(){SongId=10,NameSong="Każdy ponad każdym", Author="WWO",DateAdded= DateTime.Parse("05/11/2017 15:42:35"),
                    Description ="Opis",Price=54,NameFilePicture="kazdyponadkazdym.jpg",Bestseller=true,CategoryId=2},
            };
            songs.ForEach(x => context.Songs.AddOrUpdate(x));
            context.SaveChanges();
            
        }
    }
}