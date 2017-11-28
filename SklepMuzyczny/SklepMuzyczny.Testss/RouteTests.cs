using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Moq;
using System.Reflection;
using System.Web.Routing;


namespace SklepMuzyczny.Testss
{
   
    [TestClass]
    public class RouteTests
    {
        #region Preparing Methods
        private HttpContextBase CreateHtttpContext(string targetUrl = null,
              string httpMethod = "GET")
        {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(x => x.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockContext.Setup(x => x.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action,
            object routeProperities = null,string httpMethod = "GET")
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHtttpContext(url, httpMethod));

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperities));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller,
            string action,object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if(propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach(PropertyInfo pi in propInfo)
                {
                    if(!(routeResult.Values.ContainsKey(pi.Name)
                        && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        private void TestRouteFail(string url)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHtttpContext(url));

            Assert.IsTrue(result == null || result.Route == null);
        }
        #endregion
        #region Tests Routing
        [TestMethod]
        public void Test_Incoming_Routes()
        {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Home/StaticSites/AboutMe", "Home", "StaticSites", new { id = "AboutMe" });
            TestRouteMatch("~/Piosenki/Przyklad", "Song", "SongsByCategory", new { categoryName = "Przyklad" });
            TestRouteMatch("~/Piosenki/Przyklad/Strona-1", "Song", "SongsByCategory", new { categoryName = "Przyklad", page = "1" });
            TestRouteMatch("~/Piosenki/Szczegoly/1", "Song", "SongDetails", new { songId = "1" });
            TestRouteMatch("~/Koszyk/Twoje-produkty", "Cart", "Index");
            TestRouteMatch("~/Koszyk/Skladanie-zamowienia", "Cart", "Payment");
            TestRouteMatch("~/Koszyk/Skladnie-zamowienia/Potwierdzenie", "Cart", "Thanks");

        }
        #endregion
    }
}
