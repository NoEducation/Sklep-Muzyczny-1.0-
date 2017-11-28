using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.ViewModels
{
    public class IndexCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalValue { get; set; }
        public int TotalQuantityItems { get; set; }
       
    }
}