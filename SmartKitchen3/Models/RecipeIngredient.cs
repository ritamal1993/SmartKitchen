using System.ComponentModel.DataAnnotations;

namespace SmartKitchen3.Models
{
    public class RecipeIngredient
    {

        [Key]
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}