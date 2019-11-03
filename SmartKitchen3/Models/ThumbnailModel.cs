using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class ThumbnailModel
    {
        public int Recipeid { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        public string ImageUrl { get; set; }

        public string Link { get; set; }
    }
}