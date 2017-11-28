using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.Models;
using SklepMuzyczny.Controllers;
using SklepMuzyczny.ViewModels;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Reflection;

namespace SklepMuzyczny.Testss
{
    [TestClass]
    public class CartManagerTests
    {
        #region Preparing Methods

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://przykladWzietyZStackoverflow.com", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }

        #endregion
        #region Test Methods

        [TestMethod]
        public void Can_Add_And_Remove_Object_From_Session()
        {
            const string keyOne = "TEST_ONE";
            const string keyTwo = "TEST_TWO";
            Song[] songs = new Song[]{ new Song() { SongId =1 ,NameSong = "Test1"},
                new Song() { SongId =2 ,NameSong = "Test2"},
                new Song() { SongId =3 ,NameSong = "Test3"},
                new Song() { SongId =4 ,NameSong = "Test4"}};

            List<CartItem> cartItems = new List<CartItem>() { new CartItem() { Song = songs[0], Quantity = 1 },
                new CartItem() { Song = songs[1], Quantity = 1 },
                new CartItem() { Song = songs[2], Quantity = 1 },
                new CartItem() { Song = songs[3], Quantity = 1 },};

            List<CartItem> diffrentCartItems = new List<CartItem>() { new CartItem() { Song = songs[2], Quantity = 1 },
                new CartItem() { Song = songs[3], Quantity = 1 } };


            ISessionManager manager = new DefaultSessionManager(FakeHttpContext());

            manager.Set<List<CartItem>>(keyOne, cartItems);
            manager.Set<List<CartItem>>(keyTwo, diffrentCartItems);

            Assert.IsInstanceOfType(manager.Get<List<CartItem>>(keyOne), typeof(List<CartItem>));
            Assert.AreEqual(cartItems, manager.Get<List<CartItem>>(keyOne));
            Assert.AreEqual(manager.Get<List<CartItem>>(keyOne).Count, 4);
            Assert.IsTrue(cartItems.Equals(manager.Get<List<CartItem>>(keyOne)));

            Assert.IsInstanceOfType(manager.TryGet<List<CartItem>>(keyTwo), typeof(List<CartItem>));
            Assert.AreEqual(diffrentCartItems, manager.TryGet<List<CartItem>>(keyTwo));
            Assert.AreEqual(manager.TryGet<List<CartItem>>(keyTwo).Count, 2);
            Assert.IsTrue(diffrentCartItems.Equals(manager.Get<List<CartItem>>(keyTwo)));

            manager.Remove<CartItem>(keyOne);
            manager.Remove<CartItem>(keyTwo);

            Assert.IsNull(manager.TryGet<List<CartItem>>(keyOne));
            Assert.IsNull(manager.Get<List<CartItem>>(keyTwo));
        }

        [TestMethod]
        public void CartManger_Can_Add_Object()
        {
            List<CartItem> mySession = new List<CartItem>();
            Mock<ISessionManager> mockSession = new Mock<ISessionManager>();
            Mock<ISongRepository> mockRepository = new Mock<ISongRepository>();
            mockSession.Setup(x => x.Set<List<CartItem>>(It.IsAny<string>(),
                It.IsAny<List<CartItem>>())).Callback((string e, List<CartItem> s) => mySession = s);

            mockSession.Setup(x => x.Get<List<CartItem>>(It.IsAny<string>())).Returns(mySession);

            mockRepository.Setup(x => x.Songs).Returns(new Song[] { new Song() { SongId = 1, NameSong = "Test1" },
                new Song() { SongId = 2, NameSong = "Test2" },
                new Song() { SongId = 3, NameSong = "Test3" },
                new Song() { SongId = 4, NameSong = "Test4" }});

            CartManager target = new CartManager(mockSession.Object, mockRepository.Object);

            target.Add(1);
            target.Add(2);
            target.Add(2);

            Assert.AreEqual(2, mySession.Count);
            Assert.AreEqual(1, mySession[0].Song.SongId);
            Assert.AreEqual(2, mySession[1].Song.SongId);
            Assert.AreEqual(2, mySession[1].Quantity);

        }
        [TestMethod]
        public void CarManager_Method_TakeCartFromSession_Works_Correctly()
        {
            List<CartItem> mySession = new List<CartItem>();

            Mock<ISessionManager> mockSession = new Mock<ISessionManager>();
            Mock<ISongRepository> mockRepository = new Mock<ISongRepository>();

            mockSession.Setup(x => x.Set<List<CartItem>>(It.IsAny<string>(),
                It.IsAny<List<CartItem>>())).Callback((string e, List<CartItem> s) => mySession = s);

            mockSession.Setup(x => x.Get<List<CartItem>>(It.IsAny<string>())).Returns(mySession);
           
            mockRepository.Setup(x => x.Songs).Returns(new Song[] { new Song() { SongId = 1, NameSong = "Test1" },
                new Song() { SongId = 2, NameSong = "Test2" },
                new Song() { SongId = 3, NameSong = "Test3" },
                new Song() { SongId = 4, NameSong = "Test4" }});


            CartManager manager = new CartManager(mockSession.Object, mockRepository.Object);
            manager.Add(1);
            manager.Add(1);
            manager.Add(3);

            var result = manager.TakeCartFromSession();

            mockRepository.Verify(x => x.Songs);
            Assert.AreEqual(2, result.Count);
            Assert.IsInstanceOfType(result, typeof(List<CartItem>));
        }
        [TestMethod]
        public void CartManager_Can_Remove_Object()
        {
            List<CartItem> mySession = new List<CartItem>();
            Mock<ISessionManager> mockSession = new Mock<ISessionManager>();
            Mock<ISongRepository> mockRepository = new Mock<ISongRepository>();

            mockSession.Setup(x => x.Set<List<CartItem>>(It.IsAny<string>(),
                It.IsAny<List<CartItem>>())).Callback((string e, List<CartItem> s) => mySession = s);

            mockSession.Setup(x => x.Get<List<CartItem>>(It.IsAny<string>())).Returns(mySession);

            //mockSession.Setup(x => x.Remove<List<CartItem>>(It.IsAny<string>())).Callback(() => mySession.Clear());

            mockRepository.Setup(x => x.Songs).Returns(new Song[] { new Song() { SongId = 1, NameSong = "Test1" },
                new Song() { SongId = 2, NameSong = "Test2" },
                new Song() { SongId = 3, NameSong = "Test3" },
                new Song() { SongId = 4, NameSong = "Test4" }});

            CartManager target = new CartManager(mockSession.Object, mockRepository.Object);

            target.Add(1);
            target.Add(2);
            target.Add(1);
            target.Add(3);

            target.Remove(1);
            target.Remove(3);

            Assert.AreEqual(2, mySession.Count);
            Assert.AreEqual(1, mySession[0].Song.SongId);
            Assert.AreEqual(1, mySession[0].Quantity);
            Assert.AreEqual(2, mySession[1].Song.SongId);

        }
        [TestMethod]
        public void CartManger_Calculate_Total_Value_And_Quantity_Works_Correctly()
        {

            List<CartItem> mySession = new List<CartItem>();
            Mock<ISessionManager> mockSession = new Mock<ISessionManager>();
            Mock<ISongRepository> mockRepository = new Mock<ISongRepository>();

            mockSession.Setup(x => x.Set<List<CartItem>>(It.IsAny<string>(),
                It.IsAny<List<CartItem>>())).Callback((string e, List<CartItem> s) => mySession = s);

            mockSession.Setup(x => x.Get<List<CartItem>>(It.IsAny<string>())).Returns(mySession);

            mockRepository.Setup(x => x.Songs).Returns(new Song[] { new Song() { SongId = 1, NameSong = "Test1",Price = 20 },
                new Song() { SongId = 2, NameSong = "Test2", Price = 30 },
                new Song() { SongId = 3, NameSong = "Test3", Price = 40 },
                new Song() { SongId = 4, NameSong = "Test4", Price = 50 }});

            CartManager target = new CartManager(mockSession.Object, mockRepository.Object);
            target.Add(1);
            target.Add(2);
            target.Add(1);
            target.Add(3);

            Assert.AreEqual(110, target.CalculateTotalValue());
            Assert.AreEqual(4, target.CalculateTotalQuantity());

        }
        [TestMethod]
        public void CartManger_Create_Order_Test()
        {
            Song[] songs = new Song[]{ new Song() { SongId =1 ,NameSong = "Test1"},
                new Song() { SongId =2 ,NameSong = "Test2"},
                new Song() { SongId =3 ,NameSong = "Test3"},
                new Song() { SongId =4 ,NameSong = "Test4"}};

            List<CartItem> mySession= new List<CartItem>() { new CartItem() { Song = songs[0], Quantity = 1 },
                new CartItem() { Song = songs[1], Quantity = 1 },
                new CartItem() { Song = songs[2], Quantity = 1 },
                new CartItem() { Song = songs[3], Quantity = 1 }};

            List<Order> myOrders = new List<Order>();
            Order fakeOrder = new Order() { Name = "Test1", OrderId = 1 };
            Mock<ISessionManager> mockSession = new Mock<ISessionManager>();
            Mock<ISongRepository> mockRepository = new Mock<ISongRepository>();

            mockSession.Setup(x => x.Get<List<CartItem>>(It.IsAny<string>())).Returns(mySession);
            
            mockRepository.Setup(x => x.AddOrder(It.IsAny<Order>())).Callback((Order s) => myOrders.Add(s));
            mockRepository.Setup(x => x.OrdersWithSongOrderedAndSongIncluded).Returns(myOrders);
           

            CartManager target = new CartManager(mockSession.Object, mockRepository.Object);
            var result =  target.CreatedOrder(fakeOrder);

            Assert.IsInstanceOfType(result.SongOrdered, typeof(List<OrderPosition>));
            Assert.AreEqual(4, result.SongOrdered.Count);
            
            
        }
        #endregion
    }
}
