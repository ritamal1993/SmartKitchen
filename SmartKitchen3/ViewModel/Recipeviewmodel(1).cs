using SmartKitchen3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.ViewModel
{
    public class Recipeviewmodel_1_
    {
        [Required]
        public int Id { get; set; }

        //recipe detailes
        [Required]
        public int RecipeId { get; set; }
        public string Title { get; set; }
        //categorys
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //recipe
        public string Instructions { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public int Calories { get; set; }


        public ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}