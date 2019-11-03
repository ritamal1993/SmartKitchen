using SmartKitchen3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.ViewModel
{
    public class RecipeViewModel
    {
        public IEnumerable<Category> Categorys { get; set; }
        public Recipe Recipe { get; set; }
      
    }
}