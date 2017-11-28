using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.Models;
using SklepMuzyczny.Controllers;
using SklepMuzyczny.ViewModels;
using PagedList;
using System.Linq;

namespace SklepMuzyczny.Testss
{

    [TestClass]
    public class SongControllerTests
    {
        [TestMethod]
        public void SongDetails_Can_Take_Properly_Song()
        {
            Mock<ISongRepository> mock = new Mock<ISongRepository>();
            mock.Setup(x => x.Songs).Returns(new Song[] {
                new Song() { SongId= 1 ,NameSong="Piosenka nr 1"},
                new Song() { SongId= 2 ,NameSong="Piosenka nr 2"},
                new Song() { SongId= 3 ,NameSong="Piosenka nr 3"},
                new Song() { SongId= 4 ,NameSong="Piosenka nr 4"}});

            SongController target = new SongController(mock.Object);

            Song result = target.SongDetails(1).Model as Song;
            Song result2 = target.SongDetails(4).Model as Song;
            Assert.AreEqual(1, result.SongId);
            Assert.AreEqual(4, result2.SongId);
        }
        [TestMethod]
        public void SongsByCategory_Return_Songs_Of_All_Category()
        {
            Mock<ISongRepository> mock = new Mock<ISongRepository>();
            mock.Setup(x => x.Songs).Returns(new Song[] {
                new Song() {SongId = 1,NameSong = "Piosenka nr 1"},
                new Song() {SongId = 2,NameSong = "Piosenka nr 2"},
                new Song() {SongId = 3,NameSong = "Piosenka nr 3"},
                new Song() {SongId = 4,NameSong = "Piosenka nr 4"},
                new Song() {SongId = 5,NameSong = "Piosenka nr 5"},
                new Song() {SongId = 6,NameSong = "Piosenka nr 6"},});

            SongController target = new SongController(mock.Object);
            SongsByCategorySongViewModel model = target.SongsByCategory(SklepMuzyczny.Const.ConstValues.AllSongs, 1)
                .Model as SongsByCategorySongViewModel;

            IPagedList<Song> Songs = model.Songs as IPagedList<Song>;

            Assert.AreEqual(5, Songs.Count); // 5 elementow na jedna strone przypada
            Assert.AreEqual(1, Songs[0].SongId);
            Assert.AreEqual(5, Songs[4].SongId);
            
        }
        [TestMethod]
        public void SongsByCategory_Return_Songs_Of_Chosen_Category()
        {

            Song[] songsPop = {new Song() { SongId=1,NameSong="Piosenka nr 1"},
                    new Song() { SongId = 2, NameSong = "Piosenka nr 2"},
                    new Song() { SongId = 3, NameSong = "Piosenka nr 3"},
                    new Song() { SongId = 4, NameSong = "Piosenka nr 4"} };

            Song[] songsClassic = {new Song() { SongId=5,NameSong="Piosenka nr 5"},
                    new Song() { SongId = 6, NameSong = "Piosenka nr 6"},
                    new Song() { SongId = 7, NameSong = "Piosenka nr 7"},
                    new Song() { SongId = 8, NameSong = "Piosenka nr 8"} };

            Mock<ISongRepository> mock = new Mock<ISongRepository>();

            mock.Setup(x => x.CategoriesWithSongsIncluded).Returns(new Category[] {
                new Category(){ CategoryId = 1,NameCategory = "Test1", Song = songsPop },
                new Category(){ CategoryId = 2,NameCategory = "Test2", Song = songsClassic} });

            SongController target = new SongController(mock.Object);

            SongsByCategorySongViewModel model = target.SongsByCategory("Test1", 1)
                .Model as SongsByCategorySongViewModel;
            IPagedList<Song> Songs = model.Songs as IPagedList<Song>;

            Assert.AreEqual(Songs.Count, 4);
            Assert.AreEqual(Songs[0].SongId, 1);
            Assert.AreEqual(Songs[3].SongId, 4);
            Assert.AreEqual(model.ChoosenCategory, "Test1");

        }
    }
}
