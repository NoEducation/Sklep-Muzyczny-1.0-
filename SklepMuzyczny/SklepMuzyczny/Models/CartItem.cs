using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Models
{
    public class CartItem
    {
        public Song Song { get; set; }
        public int Quantity { get; set; }
    }
}