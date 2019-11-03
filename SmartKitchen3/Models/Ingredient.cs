using System.Collections.Generic;

namespace SmartKitchen3.Models
{

        public enum Measure
        {
            Kg, Gr, Liter, Units
        }

        public class Ingredient
        {
            public int IngredientId { get; set; }

            public string Name { get; set; }

            public Measure? Measure { get; set; }

            public ICollection<RecipeIngredient> RecipeIngredient { get; set; }

        }



    }
