using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class RecipeEvent
    {
        public enum RecipeEventType
        {
            Click, Impression, Share
        }

        public int RecipeEventId { get; set; }
        public int RecipeId { get; set; }
        public DateTime Date { get; set; }
        public RecipeEventType Type { get; set; }

        public Recipe Recipe { get; set; }
    }
}