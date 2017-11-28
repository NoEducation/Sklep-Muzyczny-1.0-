using SklepMuzyczny.Infrastructure;
using SklepMuzyczny.Models;
using SklepMuzyczny.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepMuzyczny.Controllers
{
    public class CartController : Controller
    {
        CartManager manager;
        public CartController(ISongRepository repo, ISessionManager session)
        {
            manager = new CartManager(session, repo);
        }

        public ActionResult Index()
        {
            IndexCartViewModel viewModel = new IndexCartViewModel();
            viewModel.CartItems = manager.TakeCartFromSession();
            viewModel.TotalValue = manager.CalculateTotalValue();
            viewModel.TotalQuantityItems = manager.CalculateTotalQuantity();
            return View(viewModel);
        }
        public RedirectToRouteResult Add(int songId)
        {
            manager.Add(songId);
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Remove(int songId)
        {
            manager.Remove(songId);
            return RedirectToAction("Index");
        }
        public PartialViewResult SummaryNavigation()
        {
            decimal quanitySongs = manager.CalculateTotalValue();
            return PartialView("_SummaryNavigation", quanitySongs.ToString());
        }
        public RedirectToRouteResult BuyNow(int songId)
        {
            manager.Add(songId);
            return RedirectToAction("Payment");
        }
        [HttpGet]
        public ViewResult Payment()
        {
            PaymetCartViewModel viewModel = new PaymetCartViewModel();
            viewModel.Orders = new Order() { DateCreated = DateTime.Now };
            viewModel.TotalQuantityItems = manager.CalculateTotalQuantity();
            viewModel.SongsOrdered = manager.TakeCartFromSession();
            viewModel.TotalValue = manager.CalculateTotalValue();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(PaymetCartViewModel orderDetails)
        {
            EmailSender emailSender = new EmailSender();
            if (!ModelState.IsValid)
            {   
                orderDetails.SongsOrdered = manager.TakeCartFromSession();
                orderDetails.TotalQuantityItems = manager.CalculateTotalQuantity();
                orderDetails.TotalValue = manager.CalculateTotalValue();
                return View(orderDetails);
            }
            var orderCreated = manager.CreatedOrder(orderDetails.Orders);
            emailSender.SendEmailToCustomer(orderCreated);
            emailSender.SendEmailToAdmin(orderCreated);
            manager.Clear();
            return RedirectToAction("Thanks");
        }
        public ViewResult Thanks()
        {
            return View();
        }
    }
}