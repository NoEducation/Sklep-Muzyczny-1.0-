using SklepMuzyczny.Models;
using SklepMuzyczny.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Infrastructure
{
    public class CartManager
    {
        ISessionManager sessionManager;
        ISongRepository repository;

        public CartManager(ISessionManager manager, ISongRepository repo)
        {
            sessionManager = manager;
            repository = repo;
        }

        public void Add(int songId) // dodajmey calą liste do session nie pojedyncze elementy 
        {
            var song = repository.Songs.Where(x => x.SongId == songId)
                .FirstOrDefault();
            var cartItems = TakeCartFromSession();

            if (cartItems.Find(x => x.Song.SongId == songId) != null)
            {
                cartItems.Find(x => x.Song.SongId == songId).Quantity++;
            }

            else
            {
               CartItem myCartItme = new CartItem()
                {
                    Song = song,
                    Quantity = 1
                };
                cartItems.Add(myCartItme);
         
            }
            sessionManager.Set<List<CartItem>>(Const.ConstValues.SessionKeyCart, cartItems);
        }
       
        public void Remove(int songId)
        {
            var cartItems = TakeCartFromSession();
            var song = cartItems.Find(x => x.Song.SongId == songId);
            if(song != null)
            {
                if(song.Quantity > 1)
                {
                    song.Quantity--;
                }
                else
                {
                    cartItems.Remove(song);
                }
            }
            sessionManager.Set<List<CartItem>>(Const.ConstValues.SessionKeyCart, cartItems);

        }
        public decimal CalculateTotalValue()
        {
            var cartItems = TakeCartFromSession();
            return cartItems.Sum(x => x.Quantity * x.Song.Price);
        }
        public int CalculateTotalQuantity()
        {
            var cartItems = TakeCartFromSession();
            return cartItems.Sum(x => x.Quantity);
        }

        public  List<CartItem> TakeCartFromSession()
        {
            List<CartItem> cartItems;
            if (sessionManager.Get<List<CartItem>>(Const.ConstValues.SessionKeyCart) == null)
            {
                cartItems = new List<CartItem>();
                sessionManager.Set<List<CartItem>>(Const.ConstValues.SessionKeyCart, cartItems);
            }
            else
            {
                cartItems = sessionManager.Get<List<CartItem>>(Const.ConstValues.SessionKeyCart);
            }
            return cartItems;
        }
        public void Clear()
        {
            sessionManager.Remove<CartItem>(Const.ConstValues.SessionKeyCart);
        }
        public Order CreatedOrder(Order order)
        {
           var cartItems = TakeCartFromSession();
           if(order.SongOrdered == null)
           {
                order.SongOrdered = new List<OrderPosition>();
           }

           foreach(var item in cartItems)
           {
                order.SongOrdered.Add(new OrderPosition()
                {
                    SongId = item.Song.SongId,
                    OrderId = order.OrderId,
                    Quantity = item.Quantity,
                    PricePurchase = item.Quantity * item.Song.Price,
                });
           }
           order.DateCreated = DateTime.Now;
           repository.AddOrder(order);
           return  repository.OrdersWithSongOrderedAndSongIncluded
                 .FirstOrDefault(x => x.OrderId == order.OrderId);
           
        }
        public void SendEmail(Order order)
        {
            throw new Exception();
        }

        
    }
}