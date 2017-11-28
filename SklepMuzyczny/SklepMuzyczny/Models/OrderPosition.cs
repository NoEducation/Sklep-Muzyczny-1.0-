using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.Models
{
    public class OrderPosition
    {
        public int OrderPositionId { get; set; }
        public int OrderId { get; set; }
        public int SongId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePurchase { get; set; }

        public virtual Song Song { get; set; }
        public virtual Order Order { get; set; }
    }
}