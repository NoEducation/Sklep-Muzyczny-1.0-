using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.ViewModels
{
    public class PaymetCartViewModel
    {
        public Order Orders { get; set; }
        public decimal TotalValue { get; set; }
        public int TotalQuantityItems { get; set; }
        public List<CartItem> SongsOrdered { get; set; }
    }
}