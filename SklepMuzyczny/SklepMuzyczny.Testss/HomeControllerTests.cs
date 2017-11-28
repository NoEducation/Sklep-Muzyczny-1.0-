using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.Models;
using SklepMuzyczny.Controllers;
using SklepMuzyczny.ViewModels;
using System.Collections.Generic;

namespace SklepMuzyczny.Testss
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Home_Validate_Model()
        {
            Mock<ISongRepository> mock = new Mock<ISongRepository>();
            mock.Setup(x => x.Songs).Returns(new Song[] { new Song() { SongId=0, CategoryId=1,NameSong="Moja PiosenkaNr7",
                Bestseller =true,DateAdded = DateTime.Parse("02/11/2017 18:44:01")},
            new Song() { SongId=1, CategoryId=1,NameSong="Moja PiosenkaNr1",Bestseller = true,
                DateAdded =DateTime.Parse("02/11/2017 18:44:02")},
            new Song() { SongId=2, CategoryId=1,NameSong="Moja PiosenkaNr2",Bestseller = false,
                DateAdded =DateTime.Parse("02/11/2017 18:44:03")},
            new Song() { SongId=3, CategoryId=1,NameSong="Moja PiosenkaNr3",Bestseller = true,
                DateAdded =DateTime.Parse("02/11/2017 18:44:06")},
            new Song() { SongId=4, CategoryId=1,NameSong="Moja PiosenkaNr4",Bestseller = true,
                DateAdded =DateTime.Parse("03/11/2017 18:44:07")},
            new Song() { SongId=5, CategoryId=1,NameSong="Moja PiosenkaNr5",Bestseller = true,
                DateAdded =DateTime.Parse("07/11/2017 18:44:02")},
            new Song() { SongId=6, CategoryId=1,NameSong="Moja PiosenkaNr6",Bestseller = true,
                DateAdded =DateTime.Parse("02/11/2017 18:43:02")}});

            HomeController target = new HomeController(mock.Object);

            IndexHomeViewModel result = target.Index().Model as IndexHomeViewModel;

            Assert.AreEqual(result.Bestsellers.Count, 5);
            Assert.AreEqual(result.Bestsellers[0].SongId, 0);
            Assert.AreEqual(result.Newest.Count, 5);
            
        }
        [TestMethod]
        public void MenuHorizontal_Can_Take_Categories()
        {
            Mock<ISongRepository> mock = new Mock<ISongRepository>();
            mock.Setup(x => x.Categories).Returns(new Category[] {
                new Category() { CategoryId=1,NameCategory="kategoria nr 1"},
                new Category() { CategoryId=2,NameCategory="kategoria nr 2"},
                new Category() { CategoryId=3,NameCategory="kategoria nr 3"}});

            HomeController target = new HomeController(mock.Object);

            List<Category> result= target.MenuHorizontal().Model as List<Category>;

            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0].CategoryId, 1);
        }
    }
}
