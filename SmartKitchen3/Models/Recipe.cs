using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class Recipe { 

       public int RecipeId { get; set; }

        
        [DisplayName("Recipe Name")]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string Instructions { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public int Calories { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }

        public Category Category { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredient { get; set; }

  


    }
}