using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        public int KitchenId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public Kitchen Kitchen { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}